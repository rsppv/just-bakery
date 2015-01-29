using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(StockMD))]
  public partial class Stock{}

  public class StockMD
  {
    public System.Guid StockID { get; set; }
    [Display(Name = "Тип склада")]
    public string StockType { get; set; }
    [Display(Name = "Адрес склада")]
    public string Address { get; set; }
    public Nullable<System.Guid> BakeryID { get; set; }

    public virtual ICollection<ProductAccountingLog> ProductAccountingLog { get; set; }
    public virtual ICollection<RawAccountingLog> RawAccountingLog { get; set; }
    public virtual ICollection<ProductResidue> ProductResidue { get; set; }
    public virtual ICollection<RawResidue> RawResidue { get; set; }
    public virtual Bakery Bakery { get; set; }
  }
}