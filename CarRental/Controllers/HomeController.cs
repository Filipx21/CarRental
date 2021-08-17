using CarRental.DAL;
using CarRental.Infrastructure;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        CarRentalContext db = new CarRentalContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }

    }
}