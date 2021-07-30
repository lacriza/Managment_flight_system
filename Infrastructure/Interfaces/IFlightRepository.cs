using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightRepository
  {
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task<IEnumerable<Flight>> GetFiltredPagedFlights(Filters filters);
  }
}
