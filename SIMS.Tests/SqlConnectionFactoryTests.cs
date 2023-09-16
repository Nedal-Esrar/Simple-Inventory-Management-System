using System.Data.SqlClient;
using SIMS.Repositories.SqlRepo;

namespace SIMS.Tests;

public class SqlConnectionFactoryTests
{
  private readonly SqlConnectionFactory _sut;

  private readonly string _connectionString;

  public SqlConnectionFactoryTests()
  {
    _connectionString = GenerateRandomConnectionString();

    _sut = new SqlConnectionFactory(_connectionString);
  }
  
  private string GenerateRandomConnectionString()
  {
    var fixture = new Fixture();
    
    var server = fixture.Create<string>();
    var database = fixture.Create<string>();
    var userId = fixture.Create<string>();
    var password = fixture.Create<string>();

    return $"Server={server};Database={database};User Id={userId};Password={password};";
  }

  [Fact]
  public void CreateConnection_ShouldReturnSqlConnection()
  {
    var actual = _sut.CreateConnection();

    actual.Should()
      .NotBeNull()
      .And
      .BeOfType<SqlConnection>();
  }

  [Fact]
  public void CreateConnection_ReturnConnectionShouldHaveTheExpectedConnectionString()
  {
    var actual = _sut.CreateConnection();

    actual.ConnectionString.Should().Be(_connectionString);
  }
}