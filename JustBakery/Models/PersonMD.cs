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
    [Display(Name = "Ф.И.О.")]
    public string FullName { get { return LastName+" "+FirstName+" "+MiddleName; } }

    [Display(Name = "Ф.И.О.")]
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
    [Display(Name = "Фамилия")]
    public string LastName { get; set; }
    [Display(Name = "Имя")]
    public string FirstName { get; set; }
    [Display(Name = "Отчество")]
    public string MiddleName { get; set; }
    [Display(Name = "Дата рождения")]
    public DateTime BirthDay { get; set; }
    [Display(Name = "Адрес")]
    public string Address { get; set; }
    [Display(Name = "Телефон")]
    public string Phone { get; set; }

    public virtual ICollection<Customer> Customer { get; set; }
    public virtual ICollection<Supplier> Supplier { get; set; }
    public virtual ICollection<Employee> Employee { get; set; }
  }
}