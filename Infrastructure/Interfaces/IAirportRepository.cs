using Core.POCO;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IAirportRepository
  {
    Task<IEnumerable<Airport>> GetAllAirportsAsync();  
    Task<IEnumerable<Airport>> GetPagedAirports(PagingInfo filters);
    Task<Airport> GetAirportByIATACodeAsync(string IATACode);
  }
}
