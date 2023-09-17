using SIMS.Models;

namespace SIMS.Repositories.SqlRepo;

public class SqlProductRepository : IProductRepository
{
  private readonly ISqlConnectionFactory _connectionFactory;

  private readonly IDapperWrapper _dapperWrapper;

  public SqlProductRepository(ISqlConnectionFactory connectionFactory, IDapperWrapper dapperWrapper)
  {
    _connectionFactory = connectionFactory;

    _dapperWrapper = dapperWrapper;
  }

  public async Task<IEnumerable<Product>> GetAll()
  {
    using var dbConnection = _connectionFactory.CreateConnection();

    dbConnection.Open();

    return await _dapperWrapper.GetAll(dbConnection, SqlCommands.GetAllProducts);
  }

  public async Task<Product?> GetByName(string name)
  {
    using var dbConnection = _connectionFactory.CreateConnection();
    
    dbConnection.Open();
      
    return await _dapperWrapper.GetByName(dbConnection, SqlCommands.GetProductByName, new { name });
  }

  public async Task Add(Product product)
  {
    using var dbConnection = _connectionFactory.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Add(dbConnection, SqlCommands.AddProduct, product);
  }

  public async Task Update(Product product)
  {
    using var dbConnection = _connectionFactory.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Update(dbConnection, SqlCommands.UpdateProduct, product);
  }

  public async Task Delete(Product product)
  {
    using var dbConnection = _connectionFactory.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Delete(dbConnection, SqlCommands.DeleteProduct, product);
  }
}