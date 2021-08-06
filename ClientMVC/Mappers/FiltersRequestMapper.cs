using ClientMVC.Models;
using System;

namespace ClientMVC
{
  public static class FiltersRequestMapper
  {
    public static FiltersRequest PriceRangeMap(this FiltersRequest request)
    {
      if (!string.IsNullOrEmpty(request.PriceRange))
      {
        var priceRangeArr = request.PriceRange.Split('-');
        if (priceRangeArr.Length > 0)
        {
          request.PriceFromInNIS = Convert.ToDecimal(priceRangeArr[0]);
          request.PriceToInNIS = Convert.ToDecimal(priceRangeArr[1]);
        }
      }

      return request;
    }
  }
}