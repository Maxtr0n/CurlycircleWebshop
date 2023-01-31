using AutoMapper;
using BLL.Dtos;
using BLL.Dtos.Barion;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using BLL.ViewModels.Barion;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.QueryParameters;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IWebPaymentRepository> _webPaymentRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly Mock<IBarionClient> _barionClientMock;
        private readonly Mock<IConfiguration> _configurationStub;

        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _cartRepositoryMock = new Mock<ICartRepository>();
            _webPaymentRepositoryMock = new Mock<IWebPaymentRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _barionClientMock = new Mock<IBarionClient>();
            _configurationStub = new Mock<IConfiguration>();

            _orderService = new OrderService(_orderRepositoryMock.Object, _cartRepositoryMock.Object,
                _webPaymentRepositoryMock.Object, _unitOfWorkStub.Object, _mapperStub.Object,
                _configurationStub.Object, _barionClientMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidDto_CreatesOrder()
        {
            var product1 = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
            };

            var order = new Order()
            {
                Id = 1,
                Email = "Test"
            };

            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test"
            };

            var userCart = new Cart()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        ProductId = 1,
                        Product = product1,
                        Quantity = 2,
                        Price = 1000
                    },
                    new CartItem()
                    {
                        ProductId = 2,
                        Product = product2,
                        Quantity = 1,
                        Price = 3000
                    }
                }
            };

            _mapperStub.Setup(m => m.Map<Order>(dto))
                .Returns(order);
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(userCart);
            _orderRepositoryMock.Setup(c => c.AddOrder(order))
                .Returns(1);

            var result = await _orderService.CreateOrderAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(5000, order.Total);
            Assert.Empty(userCart.CartItems);
        }

        [Fact]
        public async Task CreateOrderAsync_WithEmptyCart_CreatesOrder()
        {
            var order = new Order()
            {
                Id = 1,
                Email = "Test"
            };

            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test"
            };

            var userCart = new Cart()
            {
                CartItems = new List<CartItem>()
                {

                }
            };

            _mapperStub.Setup(m => m.Map<Order>(dto))
                .Returns(order);
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(userCart);
            _orderRepositoryMock.Setup(c => c.AddOrder(order))
                .Returns(1);

            var result = await _orderService.CreateOrderAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(0, order.Total);
            Assert.Empty(userCart.CartItems);
        }

        [Fact]
        public async Task CreateOrderAsync_WithInvalidCartId_ThrowsEntityNotFoundException()
        {
            var order = new Order()
            {
                Id = 1,
                Email = "Test"
            };

            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test"
            };

            _mapperStub.Setup(m => m.Map<Order>(dto))
                .Returns(order);
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1))
                .ThrowsAsync(new EntityNotFoundException("Test"));

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _orderService.CreateOrderAsync(dto));
        }

        [Fact]
        public async Task CreateWebPaymentRequestAsync_WithValidDto_ReturnsWebPaymentRequest()
        {
            var product1 = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
            };

            var order = new Order()
            {
                Id = 1,
                Email = "Test",
                PaymentMethod = PaymentMethod.WebPayment,
                OrderItems = new List<OrderItem>()
            };

            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test",
                PaymentMethod = PaymentMethod.WebPayment
            };

            var userCart = new Cart()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        ProductId = 1,
                        Product = product1,
                        Quantity = 2,
                        Price = 1000
                    },
                    new CartItem()
                    {
                        ProductId = 2,
                        Product = product2,
                        Quantity = 1,
                        Price = 3000
                    }
                }
            };

            var startPaymentResponse = new StartPaymentDto()
            {
                PaymentId = "1",
                PaymentRequestId = "1",
                Status = "TestStatus",
                GatewayUrl = "Test"
            };

            _mapperStub.Setup(m => m.Map<Order>(dto))
               .Returns(order);
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(userCart);
            _orderRepositoryMock.Setup(c => c.AddOrder(order))
                .Returns(1);
            _barionClientMock.Setup(b => b.StartPayment(It.IsAny<StartPaymentRequestViewModel>()).Result)
                .Returns(startPaymentResponse);

            var result = await _orderService.CreateWebPaymentRequestAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("1", result.PaymentId);
            Assert.Equal("1", result.PaymentRequestId);
            Assert.Equal("TestStatus", result.Status);
            Assert.Equal("Test", result.GatewayUrl);
            Assert.Equal(5000, order.Total);
            Assert.Empty(userCart.CartItems);
        }

        [Fact]
        public async Task CreateWebPaymentRequestAsync_WithWrongPaymentMethod_ThrowsValidationAppException()
        {
            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test",
                PaymentMethod = PaymentMethod.MoneyTransfer
            };

            await Assert.ThrowsAsync<ValidationAppException>(() => _orderService.CreateWebPaymentRequestAsync(dto));
        }

        [Fact]
        public async Task CreateWebPaymentRequestAsync_WithBarionError_ThrowsWebPaymentException()
        {
            var order = new Order()
            {
                Id = 1,
                Email = "Test",
                PaymentMethod = PaymentMethod.WebPayment,
            };

            var dto = new OrderUpsertDto()
            {
                CartId = 1,
                Email = "Test",
                PaymentMethod = PaymentMethod.WebPayment
            };

            var userCart = new Cart()
            {
                CartItems = new List<CartItem>() { }
            };

            var startPaymentResponse = new StartPaymentDto()
            {
                Errors = new List<BarionError>()
                {
                    new BarionError()
                    {
                        ErrorCode = "BadRequest"
                    }
                }
            };

            _mapperStub.Setup(m => m.Map<Order>(dto))
               .Returns(order);
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(userCart);
            _orderRepositoryMock.Setup(c => c.AddOrder(order))
                .Returns(1);
            _barionClientMock.Setup(b => b.StartPayment(It.IsAny<StartPaymentRequestViewModel>()).Result)
               .Returns(startPaymentResponse);

            await Assert.ThrowsAsync<WebPaymentException>(() => _orderService.CreateWebPaymentRequestAsync(dto));
        }

        [Fact]
        public async Task HandleWebPaymentStatusChangedAsync_WithValidPaymentId_HandlesRequest()
        {
            var paymentStateResponse = new GetPaymentStateDto()
            {
                PaymentId = "1",
                Status = "Completed"
            };

            var webPayment = new WebPayment()
            {
                Id = 1,
                PaymentStatus = "Created"
            };

            _configurationStub.Setup(c => c["Barion:SecretKey"])
                .Returns("TestSecretKey");
            _barionClientMock.Setup(b => b.GetPaymentState(It.IsAny<GetPaymentStateRequestViewModel>()).Result)
                .Returns(paymentStateResponse);
            _webPaymentRepositoryMock.Setup(w => w.GetWebPaymentByBarionPaymentIdAsync("1").Result)
                .Returns(webPayment);

            await _orderService.HandleWebPaymentStatusChangedAsync("1");

            Assert.Equal("Completed", webPayment.PaymentStatus);
            _webPaymentRepositoryMock.Verify(w => w.GetWebPaymentByBarionPaymentIdAsync("1"), Times.Once);
        }

        [Fact]
        public async Task HandleWebPaymentStatusChangedAsync_WithBarionError_ThrowsWebPaymentException()
        {
            var paymentStateResponse = new GetPaymentStateDto()
            {
                Errors = new List<BarionError>()
               {
                   new BarionError()
                   {
                      ErrorCode = "Error"
                   }
               }
            };

            var webPayment = new WebPayment()
            {
                Id = 1,
                PaymentStatus = "Created"
            };

            _configurationStub.Setup(c => c["Barion:SecretKey"])
                .Returns("TestSecretKey");
            _barionClientMock.Setup(b => b.GetPaymentState(It.IsAny<GetPaymentStateRequestViewModel>()).Result)
                .Returns(paymentStateResponse);

            await Assert.ThrowsAsync<WebPaymentException>(() => _orderService.HandleWebPaymentStatusChangedAsync("1"));
        }

        [Fact]
        public async Task GetUserOrders_WithValidId_ReturnsUserOrders()
        {
            var orders = new List<Order>()
            {
                new Order() { Id = 1, Email = "Test" },
                new Order() { Id = 2, Email = "Test" },
            };

            var vm = new OrdersViewModel()
            {
                Orders = new List<OrderViewModel>()
                {
                    new OrderViewModel() { Id = 1, Email = "Test" },
                    new OrderViewModel() { Id = 2, Email = "Test" },
                }
            };

            _orderRepositoryMock.Setup(o => o.GetUserOrdersAsync(1).Result)
                .Returns(orders);
            _mapperStub.Setup(m => m.Map<OrdersViewModel>(orders))
                .Returns(vm);

            var result = await _orderService.GetUserOrders(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Orders);
            Assert.Equal(2, result.Orders.ToList().Count);
            Assert.Equal(1, result.Orders.ToList()[0].Id);
            Assert.Equal(2, result.Orders.ToList()[1].Id);
        }

        [Fact]
        public async Task GetAllOrderOrderItemsAsync_WithValidId_ReturnsOrderItems()
        {
            var order = new Order()
            {
                Id = 1,
                OrderItems = new List<OrderItem>() { }
            };

            var vm = new OrderItemsViewModel()
            {
                OrderItems = new List<OrderItemViewModel>()
                {
                    new OrderItemViewModel()
                    {
                        Id = 1,
                        Quantity = 2,
                        Price = 2000,
                        ProductId = 1,
                        OrderId = 1
                    }
                }
            };

            _orderRepositoryMock.Setup(o => o.GetOrderByIdAsync(1).Result)
                .Returns(order);
            _mapperStub.Setup(m => m.Map<OrderItemsViewModel>(order.OrderItems))
                .Returns(vm);

            var result = await _orderService.GetAllOrderOrderItemsAsync(1);

            Assert.Single(result.OrderItems);
            Assert.Equal(1, result.OrderItems.ToList()[0].Id);
            Assert.Equal(2, result.OrderItems.ToList()[0].Quantity);
            Assert.Equal(2000, result.OrderItems.ToList()[0].Price);
            Assert.Equal(1, result.OrderItems.ToList()[0].ProductId);
            Assert.Equal(1, result.OrderItems.ToList()[0].OrderId);
        }

        [Fact]
        public async Task DeleteOrderAsync_WithValidId_DeletesOrder()
        {
            await _orderService.DeleteOrderAsync(1);

            _orderRepositoryMock.Verify(o => o.DeleteOrderAsync(1), Times.Once);
        }

        [Fact]
        public async Task FindOrderByIdAsync_WithValidId_ReturnsCorrectOrder()
        {
            var order = new Order()
            {
                Id = 1,
                Email = "TestEmail",
                FirstName = "TestFirstName"
            };

            var vm = new OrderViewModel()
            {
                Id = 1,
                Email = "TestEmail",
                FirstName = "TestFirstName"
            };

            _orderRepositoryMock.Setup(o => o.GetOrderByIdAsync(1).Result)
                .Returns(order);
            _mapperStub.Setup(m => m.Map<OrderViewModel>(order))
                .Returns(vm);

            var result = await _orderService.FindOrderByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestEmail", result.Email);
            Assert.Equal("TestFirstName", result.FirstName);
        }

        [Fact]
        public async Task GetAllOrdersAsync_WithValidQueryParameters_ReturnsOrders()
        {
            var oqp = new OrderQueryParameters()
            {

            };

            var orderList = new List<Order>()
            {
                new Order() { Id = 1, Email = "Test1"},
                new Order() { Id = 2, Email = "Test2"},
            };

            var orders = new PagedList<Order>(orderList, 2, 0, 2);

            var vm = new PagedOrdersViewModel()
            {
                Orders = new List<OrderViewModel>()
                {
                    new OrderViewModel() { Id = 1, Email = "Test1" },
                    new OrderViewModel() { Id = 2, Email = "Test2" },
                },
                PageIndex = 0,
                PageSize = 2,
                TotalCount = 2,
                TotalPages = 1
            };

            _orderRepositoryMock.Setup(o => o.GetAllAsync(oqp).Result)
                .Returns(orders);
            _mapperStub.Setup(m => m.Map<PagedOrdersViewModel>(orders))
                .Returns(vm);

            var result = await _orderService.GetAllOrdersAsync(oqp);

            Assert.NotEmpty(result.Orders); ;
            Assert.Equal(2, result.Orders.ToList().Count);
            Assert.Equal(1, result.Orders.ToList()[0].Id);
            Assert.Equal(2, result.Orders.ToList()[1].Id);
            Assert.Equal("Test1", result.Orders.ToList()[0].Email);
            Assert.Equal("Test2", result.Orders.ToList()[1].Email);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(0, result.PageIndex);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(1, result.TotalPages);
        }

        [Fact]
        public async Task GetWebPaymentResultAsync_WithValidId_ReturnsCorrectPayment()
        {
            var wp = new WebPayment()
            {
                Id = 1,
                OrderId = 1
            };

            var vm = new WebPaymentResultViewModel()
            {
                Id = 1,
                OrderId = 1
            };

            _webPaymentRepositoryMock.Setup(w => w.GetWebPaymentByBarionPaymentIdAsync("1").Result)
                .Returns(wp);
            _mapperStub.Setup(m => m.Map<WebPaymentResultViewModel>(wp))
                .Returns(vm);

            var result = await _orderService.GetWebPaymentResultAsync("1");

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.OrderId);
        }


    }
}
