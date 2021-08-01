using System;

namespace Infrastructure.Services
{
  public static class FlightNumberGenerator
  {
    public static string Generate(string depCode, string arrCode) 
    {
      Random rd = new Random();

      string result = depCode.Substring(0,1) + arrCode.Substring(0,1);
      int rand_num = rd.Next(1, 1000);
      return result + rand_num;
    }
  }
}
