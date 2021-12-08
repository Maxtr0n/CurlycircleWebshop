using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;

namespace BLL.Services
{
  public class ProductService : IProductService
  {
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(
      IProductRepository productRepository,
      IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _productRepository = productRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    public async Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto)
    {
      var product = _mapper.Map<Product>(productUpsertDto);

      var id = _productRepository.AddProduct(product);
      await _unitOfWork.SaveChangesAsync();
      return new EntityCreatedViewModel(id);
    }

    public async Task<ProductsViewModel> GetAllProductsAsync()
    {
      var products = await _productRepository.GetAllAsync();
      var productsViewModel = _mapper.Map<ProductsViewModel>(products);
      return productsViewModel;
    }

    public async Task<ProductViewModel> FindProductByIdAsync(int productId)
    {
      var product = await _productRepository.GetProductByIdAsync(productId);
      var productViewModel = _mapper.Map<ProductViewModel>(product);
      return productViewModel;
    }

    public async Task UpdateProductAsync(int productId, ProductUpsertDto productUpdateDto)
    {
      var product = await _productRepository.GetProductByIdAsync(productId);
      product.Update(_mapper.Map<Product>(productUpdateDto));
      await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _productRepository.DeleteProductAsync(productId);
      await _unitOfWork.SaveChangesAsync();
    }
  }
}
