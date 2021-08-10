using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flight = Core.POCO.Flight;

namespace Infrastructure.Interfaces
{
  public interface IFlightRepository: IRepository<Flight>
  {
    Task<int> TotalFlightsAsync();
    Task<(IEnumerable<Flight>, int)> GetFiltredPagedFlightsAsync(Filters filters);
  }
}
