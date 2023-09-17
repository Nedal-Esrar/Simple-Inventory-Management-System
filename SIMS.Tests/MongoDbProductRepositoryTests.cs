using MongoDB.Bson;
using MongoDB.Driver;
using SIMS.Models;
using SIMS.Repositories.MongoRepo;

namespace SIMS.Tests;

public class MongoDbProductRepositoryTests
{
  private readonly Mock<IMongoCollectionFactory> _collectionFactoryMock;

  private readonly Mock<IMongoCollection<Product>> _collectionMock;

  private readonly Fixture _fixture;

  private readonly Mock<IMongoDriverWrapper> _mongoDriverWrapperMock;
  private readonly MongoDbProductRepository _sut;

  public MongoDbProductRepositoryTests()
  {
    _collectionFactoryMock = new Mock<IMongoCollectionFactory>();

    _collectionMock = new Mock<IMongoCollection<Product>>();

    _collectionFactoryMock
      .Setup(x => x.GetCollection<Product>(MongoEntitiesNames.ProductsCollectionName))
      .Returns(_collectionMock.Object);

    _mongoDriverWrapperMock = new Mock<IMongoDriverWrapper>();

    _sut = new MongoDbProductRepository(_collectionFactoryMock.Object, _mongoDriverWrapperMock.Object);

    _fixture = new Fixture();

    _fixture.Register(ObjectId.GenerateNewId);
  }

  [Fact]
  public async Task GetAll_Products_ShouldReturnAllProducts()
  {
    var products = _fixture.CreateMany<Product>().ToList();

    _mongoDriverWrapperMock
      .Setup(x => x.Find(_collectionMock.Object, _ => true))
      .ReturnsAsync(products);

    var result = await _sut.GetAll();

    result.Should().BeEquivalentTo(products);
  }

  [Fact]
  public async Task GetByName_AProductWithTheSameNameExists_ShouldReturnTheExpectedProduct()
  {
    var name = _fixture.Create<string>();
    
    var product = _fixture.Create<Product>();
    
    _mongoDriverWrapperMock
      .Setup(x => x.Find(_collectionMock.Object, p => p.Name == name))
      .ReturnsAsync(new List<Product>{product});

    var actual = await _sut.GetByName(name);

    actual.Should().BeEquivalentTo(product);
  }

  [Fact]
  public async Task GetByName_AProductWithTheSameNameDoesNotExist_ShouldReturnNull()
  {
    var name = _fixture.Create<string>();

    var actual = await _sut.GetByName(name);

    actual.Should().BeNull();
  }

  [Fact]
  public async Task Add_Product_WrapperAddShouldBeInvokedOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Add(product);

    _mongoDriverWrapperMock
      .Verify(x => x.Add(_collectionMock.Object, product), Times.Once);
  }

  [Fact]
  public async Task Update_Product_WrapperUpdateShouldBeCalledOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Update(product);

    var updateDefinition = Builders<Product>.Update
      .Set(p => p.Name, product.Name)
      .Set(p => p.Price, product.Price)
      .Set(p => p.Quantity, product.Quantity);

    _mongoDriverWrapperMock
      .Verify(x => x.Update(_collectionMock.Object, 
          p => p.Id == product.Id, 
          It.Is<UpdateDefinition<Product>>(actual => actual.ToString() == updateDefinition.ToString())),
        Times.Once);
  }

  [Fact]
  public async Task Delete_Product_WrapperDeleteShouldBeCalledOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Delete(product);

    _mongoDriverWrapperMock
      .Verify(x => x.Delete(_collectionMock.Object, p => p.Id == product.Id), Times.Once);
  }
}