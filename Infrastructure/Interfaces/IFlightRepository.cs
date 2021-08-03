using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightRepository
  {
    Task<Flight> GetByIdAsync(string flightId);
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task<IEnumerable<Flight>> GetFiltredPagedFlightsAsync(Filters filters);
    Task AddFlightAsync(Flight flight);
    Task<Flight> UpdateByIdAsync(Flight flight);
    Task<int> TotalFlightsAsync();
  }
}
