using System.Data;
using System.Data.SqlClient;

namespace SIMS.Repositories.SqlRepo;

public class SqlConnectionFactory : ISqlConnectionFactory
{
  private readonly string _connectionString;

  public SqlConnectionFactory(string connectionString)
  {
    _connectionString = connectionString;
  }
  
  public IDbConnection CreateConnection()
  {
    return new SqlConnection(_connectionString);
  }
}