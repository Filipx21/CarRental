using CarRental.App_Start;
using CarRental.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CarRental.Infrastructure;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        CarRentalContext db = new CarRentalContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult List(string categoryName, string searchQuery = null)
        {
            IEnumerable<Car> cars;
            bool isAdmin = User.IsInRole("Admin");

            if (categoryName == null || categoryName.ToUpper() == "ALL")
            {
                cars = db.Cars;
            }
            else
            {
                var category = db.Categories
                    .Include("Cars")
                    .Where(cat => cat.CategoryName.ToUpper() == categoryName.ToUpper())
                    .Single();
                cars = category.Cars;
            }

            if (isAdmin)
            {
                cars = cars
                .Where(car => ((searchQuery == null
                    || car.Producer.ToLower().Contains(searchQuery.ToLower())
                    || car.Model.ToLower().Contains(searchQuery.ToLower())))
                );
            }
            else
            {
                 cars = cars
                .Where(car => ((searchQuery == null
                    || car.Producer.ToLower().Contains(searchQuery.ToLower())
                    || car.Model.ToLower().Contains(searchQuery.ToLower()))
                    && car.IsAvailable
                    && !car.IsUsed)
                );
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Cars", cars);
            }
            return View(cars);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60000)]
        public ActionResult CategoriesMenu()
        {
            var categories = db.Categories.ToList();
            return PartialView("_CarCategories", categories);
        }

        public ActionResult Details(int id)
        {
            var car = db.Cars
                .Find(id);
            return View(car);
        }

        public ActionResult FindName(string term)
        {
            var cars = db.Cars
                .Where(car => !car.IsUsed && car.IsAvailable && car.Model.ToLower().Contains(term.ToLower()))
                .Take(5)
                .Select(car => new { label = car.Model });
            return Json(cars, JsonRequestBehavior.AllowGet);
        }
    }
}