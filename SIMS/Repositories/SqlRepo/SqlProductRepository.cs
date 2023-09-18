using SIMS.Models;

namespace SIMS.Repositories.SqlRepo;

public class SqlProductRepository : IProductRepository
{
  private readonly ISqlConnectionProvider _connectionProvider;

  private readonly IDapperWrapper _dapperWrapper;

  public SqlProductRepository(ISqlConnectionProvider connectionProvider, IDapperWrapper dapperWrapper)
  {
    _connectionProvider = connectionProvider;

    _dapperWrapper = dapperWrapper;
  }

  public async Task<IEnumerable<Product>> GetAll()
  {
    using var dbConnection = _connectionProvider.CreateConnection();

    dbConnection.Open();

    return await _dapperWrapper.GetAll(dbConnection, SqlCommands.GetAllProducts);
  }

  public async Task<Product?> GetByName(string name)
  {
    using var dbConnection = _connectionProvider.CreateConnection();
    
    dbConnection.Open();
      
    return await _dapperWrapper.GetByName(dbConnection, SqlCommands.GetProductByName, new { name });
  }

  public async Task Add(Product product)
  {
    using var dbConnection = _connectionProvider.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Add(dbConnection, SqlCommands.AddProduct, product);
  }

  public async Task Update(Product product)
  {
    using var dbConnection = _connectionProvider.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Update(dbConnection, SqlCommands.UpdateProduct, product);
  }

  public async Task Delete(Product product)
  {
    using var dbConnection = _connectionProvider.CreateConnection();
    
    dbConnection.Open();

    await _dapperWrapper.Delete(dbConnection, SqlCommands.DeleteProduct, product);
  }
}