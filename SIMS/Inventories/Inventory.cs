using AutoMapper;
using SIMS.Dtos;
using SIMS.Exceptions;
using SIMS.Models;
using SIMS.Repositories;

namespace SIMS.Inventories;

public class Inventory : IInventory
{
  private readonly IProductRepository _productRepository;

  private readonly IMapper _mapper;

  public Inventory(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;

    _mapper = mapper;
  }

  public async Task AddProduct(ProductDto product)
  {
    if (await _productRepository.GetByName(product.Name) is not null)
    {
      throw new ProductAlreadyExistsException();
    }

    await _productRepository.Add(_mapper.Map<Product>(product));
  }

  public async Task<IEnumerable<ProductDto>> GetAllProducts()
  {
    var products = await _productRepository.GetAll();

    return products.Select(product => _mapper.Map<ProductDto>(product));
  }

  public async Task UpdateProduct(ProductDto product)
  {
    var productWithSameName = await _productRepository.GetByName(product.Name);

    if (productWithSameName is not null && productWithSameName.Id != product.Id)
    {
      throw new ProductAlreadyExistsException();
    }

    await _productRepository.Update(_mapper.Map<Product>(product));
  }

  public async Task<ProductDto?> GetProductByName(string name)
  {
    return _mapper.Map<ProductDto>(await _productRepository.GetByName(name));
  }

  public async Task DeleteProduct(ProductDto product)
  {
    await _productRepository.Delete(_mapper.Map<Product>(product));
  }
}