using System.Data;

namespace SIMS.Repositories.SqlRepo;

public interface ISqlConnectionProvider
{
  IDbConnection CreateConnection();
}