using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightManagmentService
  {
    Task<IEnumerable<Flight>> ListAsync();

    Task<string> AddAsync(Flight flight);

    Task<Flight> UpdateAsync(Flight flight);
    Task<PagedResponse<List<Flight>>> ListAsync(Filters filters);
  }
}
