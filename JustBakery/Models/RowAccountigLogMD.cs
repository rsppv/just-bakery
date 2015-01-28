using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{

  [MetadataType(typeof(RawAccountingLogMD))]
  partial class RawAccountingLog
  {
  }

  public class RawAccountingLogMD
  {
    [Key]
    public System.Guid LogRecordID { get; set; }
    public System.Guid OperationTypeID { get; set; }
    public System.Guid StockID { get; set; }
    public Nullable<System.Guid> SupplierID { get; set; }
    public Nullable<System.Guid> EmployeeID { get; set; }
    public System.DateTime OperationDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Stock Stock { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual OperationType OperationType { get; set; }
    public virtual ICollection<DetailRawOperation> DetailRawOperation { get; set; }
  }
}