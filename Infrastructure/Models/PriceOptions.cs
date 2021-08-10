namespace Infrastructure.Models
{
  public class PriceOptions
  {
    public const string Price = "Price";

    public string Regular { get; set; }
    public string LowCost { get; set; }
    public string Charter { get; set; }
    public string CharterFixed { get; set; }
  }
}
