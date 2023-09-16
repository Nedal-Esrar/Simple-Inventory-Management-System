using System.Data;
using Dapper;
using SIMS.Models;

namespace SIMS.Repositories.SqlRepo;

public class DapperWrapper : IDapperWrapper
{
  public async Task<IEnumerable<Product>> GetAll(IDbConnection dbConnection, string sql)
  {
    return await dbConnection.QueryAsync<Product>(sql);
  }

  public async Task<Product?> GetByName(IDbConnection dbConnection, string sql, object param)
  {
    return await dbConnection.QueryFirstOrDefaultAsync<Product>(sql, param);
  }

  public async Task Add(IDbConnection dbConnection, string sql, Product product)
  {
    await dbConnection.ExecuteAsync(sql, product);
  }

  public async Task Update(IDbConnection dbConnection, string sql, Product product)
  {
    await dbConnection.ExecuteAsync(sql, product);
  }

  public async Task Delete(IDbConnection dbConnection, string sql, Product product)
  {
    await dbConnection.ExecuteAsync(sql, product);
  }
}