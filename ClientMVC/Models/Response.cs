namespace ClientMVC.Models
{
  public class Response<T>
  {
    public T Data { get; set; }
    public string Error { get; set; }

    public bool IsSuccessfull { get; set; }
  }
}