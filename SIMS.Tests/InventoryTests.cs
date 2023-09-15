using AutoMapper;
using SIMS.Dtos;
using SIMS.Exceptions;
using SIMS.Inventories;
using SIMS.Models;
using SIMS.Repositories;

namespace SIMS.Tests;

public class InventoryTests
{
  private readonly Inventory _sut;

  private readonly Mock<IMapper> _mapperMock;

  private readonly Mock<IProductRepository> _productRepositoryMock;

  private readonly Fixture _fixture;

  public InventoryTests()
  {
    _mapperMock = new();

    _productRepositoryMock = new();

    _sut = new(_productRepositoryMock.Object, _mapperMock.Object);

    _fixture = new();
  }
  
  [Fact]
  public async Task AddProduct_ProductWithTheSameNameExists_ShouldThrowProductAlreadyExistsException()
  {
    var name = _fixture.Create<string>();

    _productRepositoryMock
      .Setup(x => x.GetByName(name))
      .ReturnsAsync(new Product{ Name = name });

    var act = async () => await _sut.AddProduct(new() { Name = name });

    await act.Should().ThrowAsync<ProductAlreadyExistsException>();
  }

  [Fact]
  public async Task AddProduct_ProductWithTheSameNameDoesNotExist_ReposAddShouldBeInvokedOnce()
  {
    var name = _fixture.Create<string>();

    _productRepositoryMock
      .Setup(x => x.GetByName(name))
      .ReturnsAsync((Product?)null);

    var dto = _fixture.Create<ProductDto>();

    var product = _fixture.Create<Product>();

    _mapperMock
      .Setup(x => x.Map<Product>(dto))
      .Returns(product);

    await _sut.AddProduct(dto);

    _productRepositoryMock.Verify(x => x.Add(product), Times.Once);
  }

  [Fact]
  public async Task GetAllProducts_AllProducts_ShouldReturnExpectedDtos()
  {
    var products = _fixture.CreateMany<Product>();
    
    _productRepositoryMock
      .Setup(x => x.GetAll())
      .ReturnsAsync(products);

    var expected = new List<ProductDto>();
    
    foreach (var product in products)
    {
      var dto = _fixture.Create<ProductDto>();
      
      _mapperMock
        .Setup(x => x.Map<ProductDto>(product))
        .Returns(dto);
      
      expected.Add(dto);
    }

    var actual = await _sut.GetAllProducts();

    actual.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public async Task UpdateProduct_AnotherProductWithTheSameNameExistsButNotTheSameProduct_ShouldThrowProductAlreadyExistsException()
  {
    var dto = _fixture.Create<ProductDto>();

    var differentId = dto.Id + _fixture.Create<int>();
    
    _productRepositoryMock
      .Setup(x => x.GetByName(dto.Name))
      .ReturnsAsync(new Product{Id = differentId});
    
    var act = async () => await _sut.AddProduct(dto);

    await act.Should().ThrowAsync<ProductAlreadyExistsException>();
  }
  
  [Fact]
  public async Task UpdateProduct_AnotherProductWithTheSameNameExistsAndIsTheSameProduct_ReposUpdateShouldBeInvokedOnce()
  {
    var dto = _fixture.Create<ProductDto>();

    _productRepositoryMock
      .Setup(x => x.GetByName(dto.Name))
      .ReturnsAsync(new Product{Id = dto.Id});

    var product = _fixture.Create<Product>();

    _mapperMock
      .Setup(x => x.Map<Product>(dto))
      .Returns(product);

    await _sut.UpdateProduct(dto);

    _productRepositoryMock.Verify(x => x.Update(product), Times.Once);
  }

  [Fact]
  public async Task UpdateProduct_AnotherProductWithTheSameNameDoesNotExist_ReposUpdateShouldBeInvokedOnce()
  {
    var dto = _fixture.Create<ProductDto>();

    _productRepositoryMock
      .Setup(x => x.GetByName(dto.Name))
      .ReturnsAsync((Product?)null);

    var product = _fixture.Create<Product>();

    _mapperMock
      .Setup(x => x.Map<Product>(dto))
      .Returns(product);

    await _sut.UpdateProduct(dto);

    _productRepositoryMock.Verify(x => x.Update(product), Times.Once);
  }

  [Fact]
  public async Task GetProductByName_ProductWithTheSameNameExists_ShouldReturnTheExpectedDto()
  {
    var name = _fixture.Create<string>();

    var product = _fixture.Create<Product>();

    _productRepositoryMock
      .Setup(x => x.GetByName(name))
      .ReturnsAsync(product);

    var dto = _fixture.Create<ProductDto>();
    
    _mapperMock
      .Setup(x => x.Map<ProductDto>(product))
      .Returns(dto);

    var actual = await _sut.GetProductByName(name);

    actual.Should().Be(dto);
  }
  
  [Fact]
  public async Task GetProductByName_ProductWithTheSameNameDoesNotExist_ShouldReturnNull()
  {
    var name = _fixture.Create<string>();

    _productRepositoryMock
      .Setup(x => x.GetByName(name))
      .ReturnsAsync((Product?)null);

    var actual = await _sut.GetProductByName(name);

    actual.Should().BeNull();
  }

  [Fact]
  public async Task DeleteProduct_Product_ReposDeleteShouldBeInvokedOnce()
  {
    var dto = _fixture.Create<ProductDto>();

    var product = _fixture.Create<Product>();
    
    _mapperMock
      .Setup(x => x.Map<Product>(dto))
      .Returns(product);

    await _sut.DeleteProduct(dto);
    
    _productRepositoryMock.Verify(x => x.Delete(product), Times.Once);
  }
}