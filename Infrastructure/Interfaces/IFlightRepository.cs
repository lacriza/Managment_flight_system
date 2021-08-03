﻿using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightRepository
  {
    Task<Flight> GetByIdAsync(string flightId);
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task AddFlightAsync(Flight flight);
    Task<Flight> UpdateByIdAsync(Flight flight);
    Task<int> TotalFlightsAsync();
    Task<(IEnumerable<Flight>, int)> GetFiltredPagedFlightsAsync(Filters filters);
  }
}
