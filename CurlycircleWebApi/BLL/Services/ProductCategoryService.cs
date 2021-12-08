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
  public class ProductCategoryService : IProductCategoryService
  {
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductCategoryService(
      IProductCategoryRepository productCategoryRepository,
      IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _productCategoryRepository = productCategoryRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    public async Task<EntityCreatedViewModel> CreateProductCategoryAsync(ProductCategoryUpsertDto productCategoryCreatetDto)
    {
      var productCategory = _mapper.Map<ProductCategory>(productCategoryCreatetDto);

      var id = _productCategoryRepository.AddProductCategory(productCategory);
      await _unitOfWork.SaveChangesAsync();
      return new EntityCreatedViewModel(id);
    }

    public async Task<ProductCategoriesViewModel> GetAllProductCategoriesAsync()
    {
      var productCategories = await _productCategoryRepository.GetAllAsync();
      var productCategoriesViewModel = _mapper.Map<ProductCategoriesViewModel>(productCategories);
      return productCategoriesViewModel;
    }

    public async Task<ProductCategoryViewModel> FindProductCategoryByIdAsync(int productCategoryId)
    {
      var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
      var productCategoryViewModel = _mapper.Map<ProductCategoryViewModel>(productCategory);
      return productCategoryViewModel;
    }

    public async Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryUpsertDto productCategoryUpdateDto)
    {
      throw new NotImplementedException();
    }

    public async Task DeleteProductCategoryAsync(int productCategoryId)
    {
      await _productCategoryRepository.DeleteProductCategoryAsync(productCategoryId);
      await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProductsViewModel> GetAllProductCategoryProductsAsync(int productCategoryId)
    {
      var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
      var productsViewModel = _mapper.Map<ProductsViewModel>(productCategory.Products);
      return productsViewModel;
    }

    public async Task<EntityCreatedViewModel> AddProductAsync(int productCategoryId, ProductUpsertDto productCreateDto)
    {
      var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
      var product = _mapper.Map<Product>(productCreateDto);
      productCategory.AddProduct(product);

      await _unitOfWork.SaveChangesAsync();
      return new EntityCreatedViewModel(product.Id);
    }
  }
}
