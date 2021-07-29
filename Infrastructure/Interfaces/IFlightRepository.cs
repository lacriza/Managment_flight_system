using Core.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightRepository
  {
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
  }
}
