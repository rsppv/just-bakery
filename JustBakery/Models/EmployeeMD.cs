using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof(EmployeeMD))]
  public partial class Employee
  {
  }

  public class EmployeeMD
  {
    public System.Guid EmployeeID { get; set; }

    [Required]
    public System.Guid PersonID { get; set; }

    [Display(Name = "Должность")]
    [Required]
    public System.Guid PositionID { get; set; }

    [Display(Name = "Пекарня")]
    public Nullable<System.Guid> BakeryID { get; set; }

    [Display(Name = "Начало работы")]
    [Required]
    public System.DateTime StartDate { get; set; }

    [Display(Name = "Дата увольнения")]
    public Nullable<System.DateTime> DismissalDate { get; set; }

    public virtual Person Person { get; set; }
    public virtual Bakery Bakery { get; set; }
    public virtual ICollection<ProductAccountingLog> ProductAccuntingLog { get; set; }
    public virtual ICollection<RawAccountingLog> RawAccountingLog { get; set; }
    public virtual Position Position { get; set; }
  }
}