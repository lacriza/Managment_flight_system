using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class AirportRepository : BaseRepo, IAirportRepository
  {
    public AirportRepository(IConfiguration config) : base(config)
    {
    }

    public async Task<IEnumerable<Airport>> GetAllAirportsAsync()
    {
      List<Airport> airports = new List<Airport>();

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand(
          "SELECT * FROM Airport", connection);

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          var airport = new Airport();
          airport.Code = rdr["code"].ToString();
          airport.Name = rdr["name"].ToString();
          airports.Add(airport);
        }
        return airports;
      }
    }

    public async Task<Airport> GetAirportByIATACodeAsync(string IATACode)
    {
      var airport = new Airport();
      string commandText = "SELECT TOP (1) [code], [name] FROM[Airport]" +
                            "WHERE [code] = @Code";

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand(commandText, connection);
        command.Parameters.Add("@Code", SqlDbType.VarChar);
        command.Parameters["@Code"].Value = IATACode;

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          airport.Code = rdr["code"].ToString();
          airport.Name = rdr["name"].ToString();
        }
        return airport;
      }
    }

    public async Task<IEnumerable<Airport>> GetPagedAirports(PagingInfo filters)
    {
      List<Airport> airports = new List<Airport>();
      string commandText = "SELECT * FROM Airport ORDER BY code " +
                            "OFFSET(@PageNumber - 1) * @RowsOfPage ROWS " +
                            "FETCH NEXT @RowsOfPage ROWS ONLY ";

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand(commandText, connection);
        AddPagingParametres(command, filters.PageNumber, filters.PageSize);

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          var airport = new Airport();
          airport.Code = rdr["code"].ToString();
          airport.Name = rdr["name"].ToString();
          airports.Add(airport);
        }
        return airports;
      }
    }
  }
}
