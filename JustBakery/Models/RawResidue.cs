//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace JustBakery.Models
{
  using System;
  using System.Collections.Generic;

  public partial class RawResidue
  {
    public System.Guid RawResidueID { get; set; }
    [Display(Name = "Склад")]
    public System.Guid StockID { get; set; }
    [Display(Name = "Сырье")]
    public System.Guid RawID { get; set; }
    [Display(Name = "Количество")]
    public int Count { get; set; }

    public virtual Stock Stocks { get; set; }
    public virtual Raw Raw { get; set; }
  }
}