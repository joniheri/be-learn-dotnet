using System.Data;
using MySqlConnector;

namespace be_learn_dotnet.Config
{
  public class DatabaseConfig(IConfiguration configuration)
  {
    private readonly IConfiguration _configuration = configuration;

    public IDbConnection CreateConnection()
    {
      return new MySqlConnection(_configuration.GetConnectionString("ConnectionDev"));
    }
  }
}
