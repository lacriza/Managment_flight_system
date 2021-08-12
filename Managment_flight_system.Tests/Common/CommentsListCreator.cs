using Core.POCO;
using System.Collections.Generic;

namespace Managment_flight_system.Tests.Common
{
  public static class CommentsListCreator
  {
    public static IEnumerable<Comment> CreateTestComments()
    {
      return new List<Comment>
      {
        new Comment{ CommentId =1, FlightType = FlightType.Regular, Text= "Regular comment."},
        new Comment{ CommentId =2, FlightType = FlightType.LowCost, Text= "LowCost comment."},
        new Comment{ CommentId =3, FlightType = FlightType.Charter, Text= "Charter comment."},
      };
    }
  }
}
