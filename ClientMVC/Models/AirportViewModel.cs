using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClientMVC.Models
{
  public class AirportViewModel
  {
    public string SelectedOption { get; set; }
    public IEnumerable<SelectListItem> SelectOptions { get; set; }
  }

  public class AirportModel
  {
    public string Code { get; set; }
    public string Name { get; set; }
  }
}