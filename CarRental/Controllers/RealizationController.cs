using CarRental.App_Start;
using CarRental.DAL;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using CarRental.ViewModels;


namespace CarRental.Controllers
{
    public class RealizationController : Controller
    {
        private CarRentalContext db = new CarRentalContext();
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


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookIt(int id)
        {
            if (Request.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());

                var car = db.Cars
                    .Where(c => c.CarId == id)
                    .Single();

                car.IsUsed = true;
                db.SaveChanges();

                var order = new Order()
                {
                    CarId = car.CarId,
                    UserId = user.Id,
                    OrderStatus = OrderStatus.ForPickup,
                    StartMileage = car.Mileage,
                    CustomerName = string.Format("{0} {1}", user.UserData.FirstName, user.UserData.LastName),
                    CustomerAddress = string.Format("{0} {1}", user.UserData.Address, user.UserData.City),
                    CustomerPhone = user.UserData.Phone,
                    CustomerEmail = user.UserData.Email,
                    DateCreated = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("List", "Realization");

            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("List", "Car") });


        }

        public ActionResult List()
        {
            bool isAdmin = User.IsInRole("Admin");
            IEnumerable<Order> orders;

            if (isAdmin)
            {
                orders = db.Orders
                    .Where(o => o.OrderStatus == OrderStatus.ForPickup || o.OrderStatus == OrderStatus.InProgress || o.OrderStatus == OrderStatus.Problems)
                    .OrderByDescending(o => o.DateCreated)
                    .ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                orders = db.Orders
                .Where(u => (u.UserId == userId))
                .Where(o => o.OrderStatus == OrderStatus.ForPickup || o.OrderStatus == OrderStatus.InProgress)
                .OrderByDescending(o => o.DateCreated)
                .ToArray();
            }

            return View(orders);
        }

        public ActionResult Leave(int id)
        {
            var order = db.Orders.Find(id);

            order.Car.IsUsed = false;
            db.Orders.Remove(order);
            db.SaveChanges();

            return RedirectToAction("List", "Realization");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Realize(int id)
        {
            var order = db.Orders.Find(id);

            order.OrderStatus = OrderStatus.InProgress;
            db.SaveChanges();

            return RedirectToAction("List", "Realization");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Finish(int id)
        {
            var order = db.Orders.Find(id);
            var days = DateTime.Now - order.DateCreated;
            var finishedOrder = new FinishedOrder()
            {
                OrderId = id,
                StopMileage = order.Car.Mileage,
                UserName = order.User.Email,
                TotalCost = days.Days * order.Car.CostPerDay,
                TotalDays = days.Days
            };

            return View(finishedOrder);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Finish(FinishedOrder finishedOrder)
        {
            var order = db.Orders.Find(finishedOrder.OrderId);

            order.OrderStatus = OrderStatus.Realized;
            order.TotalCost = finishedOrder.TotalCost;
            order.StopMileage = finishedOrder.StopMileage;
            order.Car.IsUsed = false;
            order.Car.Mileage = finishedOrder.StopMileage;
            db.SaveChanges();

            return RedirectToAction("List", "Realization");
        }

    }
}