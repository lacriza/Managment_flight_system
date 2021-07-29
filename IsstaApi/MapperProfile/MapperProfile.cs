using AutoMapper;
using Core.POCO;
using IsstaApi.Models;

namespace Web.MapperProfile
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Flight, FlightResponse>();
    }
  }
}
