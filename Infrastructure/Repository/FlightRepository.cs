using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        SqlCommand command = new SqlCommand(
          "SELECT * FROM Flight", connection);

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
        return flightList;
      }
    }

    public async Task<IEnumerable<Flight>> GetFiltredPagedFlights(Filters filters)
    {
      List<Flight> flightList = new List<Flight>();

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand(CreateFilterCommandText(filters), connection);

        SqlDataReader rdr = CreateFilterParameters(filters, command).ExecuteReader();
        while (await rdr.ReadAsync())
        {
          var flight = new Flight();
          flight.FlightNumber = rdr["flightNumber"].ToString();
          flight.ArrivalAirportIATA = rdr["arrivalAirport"].ToString();
          flight.DepartureAirportIATA = rdr["departureAirport"].ToString();
          flight.BasePriceNIS = Convert.ToDecimal(rdr["basePriceNIS"]);
          flight.TotalPriceNIS = Convert.ToDecimal(rdr["totalPriceNIS"]);
          flight.ArrivalDateTime = (DateTimeOffset)rdr["arrivalDateTime"];
          flight.DepartureDateTime = (DateTimeOffset)rdr["departureDateTime"];
          flight.FlightType = (FlightType)Convert.ToInt32(rdr["flightType"]);
          flightList.Add(flight);
        }
        return flightList;
      }
    }

    private string CreateFilterCommandText(Filters filters) 
    {
      string select = "SELECT * FROM Flight WHERE ";

      string filter = string.Empty;
      if (filters.FlightType != null) 
      {
        filter += "[flightType] = @FlightType AND ";
      }

      if (filters.DateFrom != null)
      {
        filter += "[departureDateTime] >= @DateFrom AND ";
      }

      if (filters.DateTo != null)
      {
        filter += "[arrivalDateTime] <= @DateTo AND ";
      }

      if (filters.PriceFromInNIS != null)
      {
        filter += "[totalPriceNIS] >= @PriceFrom AND ";
      }

      if (filters.PriceToInNIS != null)
      {
        filter += "[totalPriceNIS] <=  @PriceTo AND ";
      }

      if (!string.IsNullOrEmpty(filters.ToAirportIATACode))
      {
        filter += "[arrivalAirport] = @ArrivalATACode AND ";
      }

      if (!string.IsNullOrEmpty(filters.FromAirportIATACode))
      {
        filter += "[departureAirport] = @FromATACode AND ";
      }

      if (filter.EndsWith("AND ")) 
      {
        filter = filter.Substring(0, filter.Length - 4);
      }

      string paging = "ORDER BY flightNumber " +
                      "OFFSET(@PageNumber - 1) * @RowsOfPage ROWS " +
                      "FETCH NEXT @RowsOfPage ROWS ONLY ";
      var result = select + filter + paging;
      return result;
    }

    private SqlCommand CreateFilterParameters(Filters filters, SqlCommand command)
    {
      if (filters.FlightType != null)
      {
        command.Parameters.Add("@FlightType", SqlDbType.Int);
        command.Parameters["@FlightType"].Value = filters.FlightType;
      }

      if (filters.DateFrom != null)
      {
        command.Parameters.Add("@DateFrom", SqlDbType.DateTimeOffset);
        command.Parameters["@DateFrom"].Value = filters.DateFrom;
      }

      if (filters.DateTo != null)
      {
        command.Parameters.Add("@DateTo", SqlDbType.DateTimeOffset);
        command.Parameters["@DateTo"].Value = filters.DateTo;
      }

      if (filters.PriceFromInNIS != null)
      {
        command.Parameters.Add("@PriceFrom", SqlDbType.Money);
        command.Parameters["@PriceFrom"].Value = filters.PriceFromInNIS;
      }

      if (filters.PriceToInNIS != null)
      {
        command.Parameters.Add("@PriceTo", SqlDbType.Money);
        command.Parameters["@PriceTo"].Value = filters.PriceToInNIS;
      }

      if (!string.IsNullOrEmpty(filters.ToAirportIATACode))
      {
        command.Parameters.Add("@ArrivalATACode", SqlDbType.NVarChar);
        command.Parameters["@ArrivalATACode"].Value = filters.ToAirportIATACode;
      }

      if (!string.IsNullOrEmpty(filters.FromAirportIATACode))
      {
        command.Parameters.Add("@FromATACode", SqlDbType.NVarChar);
        command.Parameters["@FromATACode"].Value = filters.FromAirportIATACode;
      }

      AddPagingParametres(command, filters.PagingInfo.PageNumber, filters.PagingInfo.PageSize);
      return command;
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