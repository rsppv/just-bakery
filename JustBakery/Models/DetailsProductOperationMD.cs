using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(DetailProductOperationMD))]
  partial class DetailProductOperation
  {
  }

  public class DetailProductOperationMD
  {
    [Key]
    public System.Guid RecordID { get; set; }
    public System.Guid ProductLogRecordID { get; set; }
    public System.Guid ProductID { get; set; }
    public int Count { get; set; }

    public virtual Product Product { get; set; }
    public virtual ProductAccountingLog ProductAccountingLog { get; set; }
  }
}