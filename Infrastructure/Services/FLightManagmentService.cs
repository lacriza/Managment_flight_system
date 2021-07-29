using Core.POCO;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
  }
}
