using System.Data;
using System.Data.SqlClient;

namespace SIMS.Repositories.SqlRepo;

public class SqlConnectionProvider : ISqlConnectionProvider
{
  private readonly string _connectionString;

  public SqlConnectionProvider(string connectionString)
  {
    _connectionString = connectionString;
  }
  
  public IDbConnection CreateConnection()
  {
    return new SqlConnection(_connectionString);
  }
}