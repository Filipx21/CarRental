using CarRental.App_Start;
using CarRental.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class HistoryController : Controller
    {
        private CarRentalContext db;

        public HistoryController(CarRentalContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int CarId)
        {
            IEnumerable<Order> history = db.Orders
                .Where(o => o.Car.CarId == CarId)
                .OrderByDescending(o => o.DateCreated);

            return View(history);
        }






    }
}