using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IAirportRepository : IRepository<Airport>
  {
    Task<IEnumerable<Airport>> GetPagedAirports(PagingInfo filters);
  }
}
