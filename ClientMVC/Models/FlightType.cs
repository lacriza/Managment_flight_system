using System.Text.Json.Serialization;

namespace ClientMVC.Models
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum FlightType
  {
    Regular = 0,
    LowCost,
    Charter
  }
}