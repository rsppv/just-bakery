using System.Collections.Generic;
using System.Web.Mvc;
using JustBakery.Models;

namespace JustBakery.ViewModel
{
  public class RecipeViewModel
  {
    public Recipe Recipe { get; set; }
    public IEnumerable<Ingridient> Ingridients { get; set; }
    public SelectList ProductList { get; set; }
  }
}