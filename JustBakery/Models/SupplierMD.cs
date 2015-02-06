using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(SupplierMD))]
  public partial class Supplier
  {
  }

  public class SupplierMD
  {
    public System.Guid SupplierID { get; set; }
    
    [Display(Name = "Контактное лицо")]
    public Nullable<System.Guid> ContactPersonID { get; set; }
    
    [Display(Name = "ИНН")]
    [Required]
    public string INN { get; set; }
    
    [Display(Name = "Название краткое")]
    [Required]
    public string ShortName { get; set; }

    [Display(Name = "Название полное")]
    [Required]
    public string FullName { get; set; }
   
    [Display(Name = "Адрес")]
    [Required]
    public string Address { get; set; }
   
    [Display(Name = "Телефон офиса")]
    [Required]
    public string OfficePhone { get; set; }

    [Display(Name = "Контактное лицо")]
    public virtual Person Person { get; set; }

    public virtual ICollection<RawAccountingLog> RawAccountingLog { get; set; }
  }
}