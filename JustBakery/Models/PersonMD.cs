using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustBakery.Models
{
  [MetadataType(typeof (PersonMD))]
  public partial class Person
  {
    public string FullName { get { return LastName+" "+FirstName+" "+MiddleName; } }
    public string ShortName { get
    {
      return LastName + " " + FirstName.Substring(0, 1) + ". " + (string.IsNullOrEmpty(MiddleName)
        ? ""
        : MiddleName.Substring(0, 1) + ".");
    } }
  }
  public class PersonMD
  {
    public System.Guid PersonID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public System.DateTime BirthDay { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public virtual ICollection<Customer> Customer { get; set; }
    public virtual ICollection<Supplier> Supplier { get; set; }
    public virtual ICollection<Employee> Employee { get; set; }
  }
}