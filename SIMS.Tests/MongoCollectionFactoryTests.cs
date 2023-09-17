using MongoDB.Driver;
using SIMS.Models;
using SIMS.Repositories.MongoRepo;

namespace SIMS.Tests;

public class MongoCollectionFactoryTests
{
  private readonly MongoCollectionFactory _sut;

  private readonly Mock<IMongoDatabase> _mongoDbMock;

  private readonly Mock<IMongoCollection<Product>> _mongoCollectionMock;

  private readonly Fixture _fixture;

  public MongoCollectionFactoryTests()
  {
    _mongoDbMock = new Mock<IMongoDatabase>();
    
    _mongoCollectionMock = new Mock<IMongoCollection<Product>>();

    _mongoDbMock
      .Setup(x => x.GetCollection<Product>(It.IsAny<string>(), null))
      .Returns(_mongoCollectionMock.Object);

    _sut = new MongoCollectionFactory(_mongoDbMock.Object);

    _fixture = new Fixture();
  }

  [Fact]
  public void GetCollection_CollectionName_ReturnsExpectedCollection()
  {
    var actual = _sut.GetCollection<Product>(_fixture.Create<string>());

    actual.Should().BeSameAs(_mongoCollectionMock.Object);
  }
}