using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
  public static class PaginationHelper
  {
    public static PagedResponse<List<T>> CreatePagedReponse<T>(this List<T> pagedData, int currentPage, int pageSize, int totalRecords)
    {
      var respose = new PagedResponse<List<T>>(pagedData, currentPage, pageSize);
      var totalPages = ((double)totalRecords / (double)pageSize);
      int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
      respose.NextPage = currentPage >= 1 && currentPage < roundedTotalPages;
      respose.PreviousPage = currentPage - 1 >= 1 && currentPage <= roundedTotalPages;
      respose.TotalPages = roundedTotalPages;
      respose.TotalRecords = totalRecords;
      return respose;
    }
  }
}
