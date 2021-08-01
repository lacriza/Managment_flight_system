using Core.POCO;

namespace Infrastructure.Services
{
  public static class CalculatePrice
  {
    public static decimal CalculateTotalPrice(this decimal basePrice, FlightType flightType)
    {
      return flightType switch
      {
        FlightType.Regular => basePrice * 1.17M,
        FlightType.LowCost => basePrice * 0.9M,
        FlightType.Charter => (basePrice * 0.95M) + 15,
        _ => basePrice,
      };
    }
  }
}
