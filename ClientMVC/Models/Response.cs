using System.Collections.Generic;

namespace ClientMVC.Models
{
  public class Response<T>
  {
    public T Data { get; set; }
    public Dictionary<string, string> Error { get; set; }

    public bool IsSuccessfull { get; set; }
  }
}