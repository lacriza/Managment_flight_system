using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
  public class BaseRepo
  {
    private readonly IConfiguration config;

    public BaseRepo(IConfiguration config)
    {
      this.config = config;
    }

    public SqlConnection GetOpenConnection()
    {
      string cs = config["ConnectionStrings:ISSTA"];
      SqlConnection connection = new SqlConnection(cs);
      connection.Open();
      return connection;
    }

    public void AddPagingParametres(SqlCommand command, int pageNumber, int pageSize) 
    {
      command.Parameters.Add("@PageNumber", SqlDbType.Int);
      command.Parameters["@PageNumber"].Value = pageNumber;
      command.Parameters.Add("@RowsOfPage", SqlDbType.Int);
      command.Parameters["@RowsOfPage"].Value = pageSize;
    }
  }
}
