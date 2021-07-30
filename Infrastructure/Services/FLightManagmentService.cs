using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
  public class FLightManagmentService : IFlightManagmentService
  {
    private readonly IFlightRepository _flightRepository;

    public FLightManagmentService(IFlightRepository flightRepository)
    {
      _flightRepository = flightRepository;
    }

    public async Task<IEnumerable<Flight>> ListAsync()
    {
        return await _flightRepository.GetAllFlightsAsync();
    }

    public async Task<IEnumerable<Flight>> ListAsync(Filters filters)
    {
      //Todo: check if IATA Code Exist in database
      return await _flightRepository.GetFiltredPagedFlights(filters);
    }
  }
}
