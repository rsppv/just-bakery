using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(ProductTypeMD))]
  public partial class ProductType
  {
    public class ProductTypeMD
    {
      public System.Guid ProductTypeID { get; set; }
      [Display(Name = "Категория")]
      public string Type { get; set; }

      public virtual ICollection<Product> Products { get; set; }
    }
  }

}