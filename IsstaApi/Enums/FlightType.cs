using System.Text.Json.Serialization;

namespace IsstaApi.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum FlightType
  {
    Regular = 0,
    LowCost,
    Charter
  }
}