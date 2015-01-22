using System.Collections.Generic;

namespace JustBakery.Models
{
  public class ProductViewModel
  {
    public IEnumerable<ProductType> Categories { get; set; }
    public IEnumerable<Product> Products { get; set; }
  }
}