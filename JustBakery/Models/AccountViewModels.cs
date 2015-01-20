using System;
using System.ComponentModel.DataAnnotations;

namespace JustBakery.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }
  }

  public class ManageUserViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Текущий пароль")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение нового пароля")]
    [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Display(Name = "Фамилия")]
    public string LastName { get; set; }
    [Display(Name = "Имя")]
    public string FirstName { get; set; }
    [Display(Name = "Отчество")]
    public string MiddleName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение пароля")]
    [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
    public string ConfirmPassword { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Дата рождения")]
    public DateTime BirthDay { get; set; }


  }
}
