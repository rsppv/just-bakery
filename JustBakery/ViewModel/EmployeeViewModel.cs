using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBakery.Models;

namespace JustBakery.ViewModel
{
  public class EmployeeViewModel
  {
    [Display(Name = "Имя")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "Отчество")]
    public string MiddleName { get; set; }

    [Display(Name = "Фамилия")]
    [Required]
    public string LastName { get; set; }

    [Display(Name = "Дата рождения")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDay { get; set; }

    [Display(Name = "Адрес")]
    public string Address { get; set; }
    
    [Display(Name = "Телефон")]
    public string Phone { get; set; }

    [Display(Name = "Дата трудоустройства")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    public Guid PositionId { get; set; }
    public Guid? BakeryId { get; set; }
    public Guid EmployeeId { get; set; }

    public SelectList PositionList { get; set; }
    public SelectList BakeryList { get; set; }
  }
}