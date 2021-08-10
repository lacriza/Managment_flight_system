using AutoMapper;
using Core.POCO;
using Infrastructure.Models;
using IsstaApi.Models;
using Web.Requests;
using Flight = Infrastructure.Models.Flight;

namespace Web.MapperProfile
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Airport, AirportResponse>();
      CreateMap<AddFlightRequest, Flight>();
      CreateMap<UpdateFlightRequest, Flight>();
      CreateMap<PagingRequest, PagingInfo>();
      CreateMap<FiltersRequest, Filters>();
      CreateMap<Flight, Core.POCO.Flight>();
    }
  }
}
