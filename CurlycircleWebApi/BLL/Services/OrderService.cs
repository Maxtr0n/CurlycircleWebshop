using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.QueryParameters;
using Domain.QueryParameters.Barion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IWebPaymentRepository _webPaymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBarionClient _barionClient;

        private IConfiguration Configuration { get; }

        public OrderService(
          IOrderRepository orderRepository,
          ICartRepository cartRepository,
          IWebPaymentRepository webPaymentRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IConfiguration configuration,
          IBarionClient barionClient
          )
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _webPaymentRepository = webPaymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Configuration = configuration;
            _barionClient = barionClient;
        }

        public async Task<EntityCreatedViewModel> CreateOrderAsync(OrderUpsertDto orderUpsertDto)
        {
            var order = _mapper.Map<Order>(orderUpsertDto);
            var userCart = await _cartRepository.GetCartByIdAsync(orderUpsertDto.CartId);
            PrepareOrder(order, userCart);

            var id = _orderRepository.AddOrder(order);
            userCart.ClearCart();

            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task<WebPaymentRequestViewModel> CreateWebPaymentRequestAsync(OrderUpsertDto orderUpsertDto)
        {
            if (orderUpsertDto.PaymentMethod != PaymentMethod.WebPayment)
            {
                throw new ValidationAppException("Web payment attempt failed.", new[]
                {
                    "Web payment was not choosen as payment method."
                });
            }

            var order = _mapper.Map<Order>(orderUpsertDto);
            var userCart = await _cartRepository.GetCartByIdAsync(orderUpsertDto.CartId);

            PrepareOrder(order, userCart);
            order.WebPayment = new()
            {
                Total = order.Total,
                PaymentStatus = "Created"
            };

            var orderId = _orderRepository.AddOrder(order);
            var request = PrepareWebPaymentRequest(order);

            StartPaymentResponse? startPaymentResponse = await _barionClient.StartPayment(request);

            if (startPaymentResponse == null)
            {
                order.WebPayment.PaymentStatus = "Failed";

                throw new WebPaymentException("Web payment attempt failed.", new[]
                {
                    "Barion server is unresponsive."
                });
            }

            if (startPaymentResponse.Errors.Count > 0)
            {
                order.WebPayment.PaymentStatus = "Failed";

                var errors = new List<string>();
                foreach (var error in startPaymentResponse.Errors)
                {
                    errors.Add(error.Title);
                }

                throw new WebPaymentException("Web payment attempt failed.", errors);
            }

            order.WebPayment.PaymentStatus = startPaymentResponse.Status;
            order.WebPayment.BarionPaymentId = startPaymentResponse.PaymentId;
            userCart.ClearCart();

            WebPaymentRequestViewModel webPaymentRequestViewModel = new()
            {
                PaymentId = startPaymentResponse.PaymentId,
                PaymentRequestId = startPaymentResponse.PaymentRequestId,
                Status = startPaymentResponse.Status,
                GatewayUrl = startPaymentResponse.GatewayUrl
            };

            await _unitOfWork.SaveChangesAsync();
            return webPaymentRequestViewModel;
        }

        public async Task HandleWebPaymentStatusChangedAsync(string paymentId)
        {
            GetPaymentStateRequest getPaymentStateRequest = new()
            {
                POSKey = Configuration["Barion:SecretKey"],
                PaymentId = paymentId
            };

            var response = await _barionClient.GetPaymentState(getPaymentStateRequest);

            if (response == null)
            {
                throw new WebPaymentException("Web payment attempt failed.", new[]
                {
                    "Barion server is unresponsive."
                });
            }

            if (response.Errors.Count > 0)
            {
                var errors = new List<string>();
                foreach (var error in response.Errors)
                {
                    errors.Add(error.Title);
                }

                throw new WebPaymentException("Web payment attempt failed.", errors);
            }

            var webPayment = await _webPaymentRepository.GetWebPaymentByBarionPaymentIdAsync(response.PaymentId);
            webPayment.PaymentStatus = response.Status;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<WebPaymentResultViewModel> GetWebPaymentResultAsync(string barionPaymentId)
        {
            var webPayment = await _webPaymentRepository.GetWebPaymentByBarionPaymentIdAsync(barionPaymentId);

            var webPaymentResultViewModel = _mapper.Map<WebPaymentResultViewModel>(webPayment);
            return webPaymentResultViewModel;
        }

        public async Task<PagedOrdersViewModel> GetAllOrdersAsync(OrderQueryParameters orderQueryParameters)
        {
            var orders = await _orderRepository.GetAllAsync(orderQueryParameters);

            var ordersViewModel = _mapper.Map<PagedOrdersViewModel>(orders);
            return ordersViewModel;
        }

        public async Task<OrderViewModel> FindOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public async Task UpdateOrderAsync(int orderId, OrderUpsertDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            order = _mapper.Map<Order>(orderUpdateDto);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<OrderItemsViewModel> GetAllOrderOrderItemsAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var orderItemsViewModel = _mapper.Map<OrderItemsViewModel>(order.OrderItems);
            return orderItemsViewModel;
        }

        public async Task<OrdersViewModel> GetUserOrders(int userId)
        {
            var orders = await _orderRepository.GetUserOrdersAsync(userId);
            var ordersViewModel = _mapper.Map<OrdersViewModel>(orders);
            return ordersViewModel;
        }

        private static void PrepareOrder(Order order, Cart userCart)
        {
            order.OrderDateTime = DateTime.Now;
            order.Total = 0;

            foreach (var cartItem in userCart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Product = cartItem.Product,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                };
                order.OrderItems.Add(orderItem);
                order.Total += cartItem.Quantity * cartItem.Price;
            }
        }

        private StartPaymentRequest PrepareWebPaymentRequest(Order order)
        {
            List<Item> items = new();

            foreach (var orderItem in order.OrderItems)
            {
                var item = new Item
                {
                    Name = orderItem.Product.Name,
                    Description = orderItem.Product.Description ?? "Nincs leírás az adott termékhez.",
                    Quantity = orderItem.Quantity,
                    UnitPrice = (decimal)orderItem.Price,
                    ItemTotal = (decimal)(orderItem.Quantity * orderItem.Price)
                };

                items.Add(item);
            }

            PaymentTransaction paymentTransaction = new()
            {
                POSTransactionId = order.WebPayment?.POSTransactionId ?? Guid.NewGuid(),
                Total = (decimal)order.Total,
                Items = items
            };

            StartPaymentRequest request = new()
            {
                POSKey = Configuration["Barion:SecretKey"],
                PaymentRequestId = order.WebPayment?.Id.ToString() ?? string.Empty,
                Transactions = new List<PaymentTransaction> { paymentTransaction },
                RedirectUrl = Configuration["Barion:RedirectUrl"],
                CallbackUrl = Configuration["Barion:CallbackUrl"],
                OrderNumber = order.Id.ToString(),
            };

            return request;
        }
    }
}
