using System.Collections.Generic;
using JustBakery.Models;

namespace JustBakery.ViewModel
{
  public class ProductDetailsViewModel
  {
    public Product Product { get; set; }
    public IEnumerable<Recipe> Recipes { get; set; }
  }
}