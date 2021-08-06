using System.Collections.Generic;

namespace ClientMVC.Models
{
  public class PagedResponse<T>
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool NextPage { get; set; }
    public bool PreviousPage { get; set; }
    public List<T> Data { get; set; }
  }
}