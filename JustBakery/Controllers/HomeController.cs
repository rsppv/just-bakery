using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBakery.Models;

namespace JustBakery.Controllers
{
  public class HomeController : Controller
  {
    private BakeryEntitiesHome db = new BakeryEntitiesHome();

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public ActionResult Vacancies()
    {
      List<Vacancies> vacancies = new List<Vacancies>();
      foreach (Position position in db.Positions)
      {
        vacancies.AddRange(db.VacancyListByPosition(position.PositionID));
      }
      vacancies.Select(
        v =>
          new
          {
            db.Positions.Find(v.PositionID).FullName,
            db.Bakeries.Find(v.BakeryID).Name,
            db.Bakeries.Find(v.BakeryID).FullAddress,
            v.VacancyCount
          });
      return View(vacancies);
    }
  }
}