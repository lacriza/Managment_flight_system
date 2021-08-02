using AutoMapper;
using Core.POCO;
using Infrastructure.Models;
using IsstaApi.Models;
using Web.Requests;

namespace Web.MapperProfile
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Flight, FlightResponse>();
      CreateMap<Airport, AirportResponse>();
      CreateMap<AddFlightRequest, Flight>();
      CreateMap<UpdateFlightRequest, Flight>();
      CreateMap<PagingRequest, PagingInfo>();
      CreateMap<FiltersRequest, Filters>();
    }
  }
}
