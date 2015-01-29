using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(ProductAccountingLogMD))]
  partial class ProductAccountingLog
  {
  }

  public class ProductAccountingLogMD
  {
    [Key]
    public System.Guid LogRecordID { get; set; }
    [Required]
    [Display(Name = "Операция")]
    public System.Guid OperationTypeID { get; set; }
    [Required]
    [Display(Name = "Склад")]
    public System.Guid StockID { get; set; }
    [Display(Name = "Покупатель")]
    public Nullable<System.Guid> CustomerID { get; set; }
    [Display(Name = "Сотрудник")]
    public Nullable<System.Guid> EmployeeID { get; set; }
    [Required]
    [Display(Name = "Дата операции")]
    public System.DateTime OperationDate { get; set; }
    [Display(Name = "Отменена")]
    public bool IsDeleted { get; set; }

    public virtual Stock Stock { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual OperationType OperationType { get; set; }
    public virtual ICollection<DetailProductOperation> DetailProductOperation { get; set; }
  }
}