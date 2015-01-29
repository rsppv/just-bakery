using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{

  [MetadataType(typeof(ProductMD))]
  public partial class Product { }
  
  public class ProductMD
  {
    public System.Guid ProductID { get; set; }
    public System.Guid ProductTypeID { get; set; }

    [Display(Name = "Цена")]
    [Required, Range(10,9999)]
    public Nullable<int> Cost { get; set; }

    [Display(Name = "Ед. изм.")]
    [Required]
    public string Units { get; set; }
    [Display(Name = "")]
    public byte[] Image { get; set; }
    
    [Required]
    [Display(Name = "Название")]
    public string Name { get; set; }

    public virtual ICollection<ProductResidue> ProductResidues { get; set; }
    public virtual ProductType ProductType { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; }
    public virtual ICollection<DetailProductOperation> DetailProductOperation { get; set; }
  }
}