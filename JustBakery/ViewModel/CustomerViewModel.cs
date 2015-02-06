using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JustBakery.Models;

namespace JustBakery.ViewModel
{
  public class CustomerViewModel
  {
    public Person Person { get; set; }
    public Customer Customer { get; set; }
  }
}