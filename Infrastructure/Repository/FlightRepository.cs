using Core.POCO;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class FlightRepository : BaseRepo, IFlightRepository
  {
    public FlightRepository(IConfiguration config) : base(config)
    {
    }

    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
      List<Flight> flightList = new List<Flight>();

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand("SELECT * FROM Flight", connection);
        //command.Connection.Open()

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          var flight = new Flight();
          flight.FlightNumber = rdr["flightNumber"].ToString();
          flight.ArrivalAirportIATA = rdr["arrivalAirport"].ToString();
          flight.DepartureAirportIATA = rdr["departureAirport"].ToString();
          flight.BasePriceNIS = Convert.ToDecimal(rdr["basePriceNIS"]);
          flight.BasePriceNIS = Convert.ToDecimal(rdr["totalPriceNIS"]);
          flight.ArrivalDateTime = (DateTimeOffset) rdr["arrivalDateTime"];
          flight.DepartureDateTime = (DateTimeOffset) rdr["departureDateTime"];
          flight.FlightType = (FlightType)Convert.ToInt32(rdr["flightType"]);
          flightList.Add(flight);
        }

        //connection.Close();
        return flightList;
      }
    }

    public IQueryable<Flight> GetFiltredFlights()
    {
      throw new NotImplementedException();
    }

    public void Insert(Flight Flight)
    {
      throw new NotImplementedException();
    }

    public void Update(Flight Flight)
    {
      throw new NotImplementedException();
    }
  }
}