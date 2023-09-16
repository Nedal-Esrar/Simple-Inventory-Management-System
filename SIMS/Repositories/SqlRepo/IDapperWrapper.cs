using System.Data;
using SIMS.Models;

namespace SIMS.Repositories.SqlRepo;

public interface IDapperWrapper
{
  Task<IEnumerable<Product>> GetAll(IDbConnection dbConnection, string sql);

  Task<Product?> GetByName(IDbConnection dbConnection, string sql, object param);

  Task Add(IDbConnection dbConnection, string sql, Product product);

  Task Update(IDbConnection dbConnection, string sql, Product product);

  Task Delete(IDbConnection dbConnection, string sql, Product product);
}