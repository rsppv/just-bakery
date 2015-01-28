using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(DetailRawOperationMD))]
  public partial class DetailRawOperation
  {
  }

  public class DetailRawOperationMD
  {
    [Key]
    public System.Guid RecordID { get; set; }
    public System.Guid RawLogRecordID { get; set; }
    public System.Guid RawID { get; set; }
    public int Count { get; set; }

    public virtual Raw Raw { get; set; }
    public virtual RawAccountingLog RawAccountingLog { get; set; }    
  }
}