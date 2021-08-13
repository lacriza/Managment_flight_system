using IsstaApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Requests
{
  public class CommentRequest
  {
    /// <summary>
    /// Id
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter.
    /// </summary>
    [EnumDataType(typeof(FlightType))]
    public FlightType FlightType { get; set; }

    /// <summary>
    /// Comment
    /// </summary>
    public string Text { get; set; }
  }
}
