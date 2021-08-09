namespace ClientMVC.Models
{
  public class PaginationViewModel
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool NextPage { get; set; }
    public bool PreviousPage { get; set; }
  }
}
