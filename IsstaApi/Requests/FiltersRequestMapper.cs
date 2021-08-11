using System;
using Web.Requests;

namespace Web.MapperProfile
{
  public static class FiltersRequestMapper
  {
    public static FiltersRequest PriceRangeMap(this FiltersRequest request) 
    {
      if (!string.IsNullOrEmpty(request.PriceRange))
      { var priceRangeArr = request.PriceRange.Split('-');
        if (priceRangeArr.Length > 0)
        {
          string trimmedFrom = priceRangeArr[0].TrimEnd(' ', '₪');
          request.PriceFromInNIS = Convert.ToDecimal(trimmedFrom);
          string trimmed = priceRangeArr[1].TrimEnd(' ', '₪');
          request.PriceToInNIS = Convert.ToDecimal(trimmed);
        }
      }

      return request; 
    }
  }
}
