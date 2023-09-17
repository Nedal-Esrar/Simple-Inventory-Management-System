using System.Data;
using SIMS.Models;
using SIMS.Repositories.SqlRepo;

namespace SIMS.Tests;

public class SqlProductRepositoryTests
{
  private readonly SqlProductRepository _sut;

  private readonly Mock<IDbConnection> _dbConnectionMock;
  
  private readonly Mock<IDapperWrapper> _dapperWrapper;
  
  private readonly Fixture _fixture;

  public SqlProductRepositoryTests()
  {
    _dapperWrapper = new Mock<IDapperWrapper>();
    
    _dbConnectionMock = new Mock<IDbConnection>();
    
    var connectionFactoryMock = new Mock<ISqlConnectionFactory>();

    connectionFactoryMock
      .Setup(x => x.CreateConnection())
      .Returns(_dbConnectionMock.Object);
    
    _sut = new SqlProductRepository(connectionFactoryMock.Object, _dapperWrapper.Object);

    _fixture = new Fixture();
  }
  
  [Fact]
  public async Task GetAll_AllProducts_ShouldReturnExpectedProducts()
  {
    var products = _fixture.CreateMany<Product>();
    
    _dapperWrapper
      .Setup(x => x.GetAll(_dbConnectionMock.Object, SqlCommands.GetAllProducts))
      .ReturnsAsync(products);

    var actual = await _sut.GetAll();

    actual.Should().BeSameAs(products);
  }
  
  [Fact]
  public async Task GetAll_AllProducts_ShouldOpenTheConnectionBeforeExecution()
  {
    _dapperWrapper
      .Setup(x => x.GetAll(_dbConnectionMock.Object, SqlCommands.GetAllProducts))
      .Callback(() => _dbConnectionMock.Verify(x => x.Open(), Times.Once));

    await _sut.GetAll();
  }

  [Fact]
  public async Task GetByName_ProductWithTheSameNameExists_ShouldReturnTheExpectedProduct()
  {
    var product = _fixture.Create<Product>();
    
    _dapperWrapper
      .Setup(x => x.GetByName(_dbConnectionMock.Object, SqlCommands.GetProductByName, 
        It.Is<object>(param => ParamMatchesName(param, product.Name))))
      .ReturnsAsync(product);
    
    var actual = await _sut.GetByName(product.Name);

    actual.Should().BeSameAs(product);
  }
  
  private static bool ParamMatchesName(object param, string expectedName)
  {
    return param.GetType().GetProperty("name")?.GetValue(param)?.ToString() == expectedName;
  }
  
  [Fact]
  public async Task GetByName_ProductWithTheSameNameDoeNotExists_ShouldReturnNull()
  {
    var actual = await _sut.GetByName(_fixture.Create<string>());

    actual.Should().BeNull();
  }
  
  [Fact]
  public async Task GetByName_ProductName_ShouldOpenTheConnectionBeforeExecution()
  {
    var name = _fixture.Create<string>();
    
    _dapperWrapper
      .Setup(x => x.GetByName(_dbConnectionMock.Object, SqlCommands.GetProductByName, 
        It.Is<object>(param => ParamMatchesName(param, name))))
      .Callback(() => _dbConnectionMock.Verify(x => x.Open(), Times.Once));

    await _sut.GetByName(name);
  }
  
  [Fact]
  public async Task Add_Product_ShouldInvokeDapperWrapperAddOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Add(product);
    
    _dapperWrapper.Verify(x => x.Add(_dbConnectionMock.Object, SqlCommands.AddProduct, product), Times.Once);
  }
  
  [Fact]
  public async Task Add_Product_ShouldOpenTheConnectionBeforeExecution()
  {
    var product = _fixture.Create<Product>();

    _dapperWrapper
      .Setup(x => x.Add(_dbConnectionMock.Object, SqlCommands.AddProduct, product))
      .Callback(() => _dbConnectionMock.Verify(x => x.Open(), Times.Once));

    await _sut.Add(product);
  }
  
  [Fact]
  public async Task Update_Product_ShouldInvokeDapperWrapperUpdateOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Update(product);
    
    _dapperWrapper.Verify(x => x.Update(_dbConnectionMock.Object, SqlCommands.UpdateProduct, product), Times.Once);
  }
  
  [Fact]
  public async Task Update_Product_ShouldOpenTheConnectionBeforeExecution()
  {
    var product = _fixture.Create<Product>();

    _dapperWrapper
      .Setup(x => x.Update(_dbConnectionMock.Object, SqlCommands.UpdateProduct, product))
      .Callback(() => _dbConnectionMock.Verify(x => x.Open(), Times.Once));

    await _sut.Update(product);
  }
  
  [Fact]
  public async Task Delete_Product_ShouldInvokeDapperWrapperDeleteOnce()
  {
    var product = _fixture.Create<Product>();

    await _sut.Delete(product);
    
    _dapperWrapper.Verify(x => x.Delete(_dbConnectionMock.Object, SqlCommands.DeleteProduct, product), Times.Once);
  }
  
  [Fact]
  public async Task Delete_Product_ShouldOpenTheConnectionBeforeExecution()
  {
    var product = _fixture.Create<Product>();

    _dapperWrapper
      .Setup(x => x.Delete(_dbConnectionMock.Object, SqlCommands.DeleteProduct, product))
      .Callback(() => _dbConnectionMock.Verify(x => x.Open(), Times.Once));

    await _sut.Delete(product);
  }
}