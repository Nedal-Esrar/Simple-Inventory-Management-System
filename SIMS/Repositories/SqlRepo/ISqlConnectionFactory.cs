using System.Data;

namespace SIMS.Repositories.SqlRepo;

public interface ISqlConnectionFactory
{
  IDbConnection CreateConnection();
}