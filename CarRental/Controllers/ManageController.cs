using CarRental.App_Start;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using CarRental.ViewModels;
using CarRental.Models;
using CarRental.DAL;
using System.Data.Entity;
using System.IO;

namespace CarRental.Controllers
{
    public class ManageController : Controller
    {
        private ApplicationUserManager _userManager;
        private CarRentalContext db = new CarRentalContext();

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            IEnumerable<Order> historyOrders;
            var userId = User.Identity.GetUserId();
            historyOrders = db.Orders
                .Where(u => (u.UserId == userId))
                .Where(o => o.OrderStatus == OrderStatus.Realized)
                .OrderByDescending(o => o.DateCreated)
                .Take(15)
                .ToArray();
            return View(historyOrders);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword", model);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index");
            }
            AddErrors(result);

            return RedirectToAction("Index");
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        public async Task<ActionResult> ChangeUserData()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return View("Error");
            }

            var VM = new ChangeUserDataViewModel()
            {
                FirstName = user.UserData.FirstName,
                LastName = user.UserData.LastName,
                Street = user.UserData.Street,
                Address = user.UserData.Address,
                City = user.UserData.City,
                ZipCode = user.UserData.ZipCode,
                Phone = user.UserData.Phone
            };
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserData(ChangeUserDataViewModel userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                user.UserData.FirstName = userData.FirstName;
                user.UserData.LastName = userData.LastName;
                user.UserData.Street = userData.Street;
                user.UserData.Address = userData.Address;
                user.UserData.City = userData.City;
                user.UserData.ZipCode = userData.ZipCode;
                user.UserData.Phone = userData.Phone;

                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", userData);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddCar(int? CarId, bool? confirm)
        {
            Car car;

            if (CarId.HasValue)
            {
                ViewBag.EditMode = 1;
                car = db.Cars.Find(CarId);
                Console.WriteLine(car.DateAdded);
            }
            else
            {
                ViewBag.EditMode = 0;
                car = new Car();
            }

            var VM = new EditCarViewModel()
            {
                Car = car,
                Categories = db.Categories
                    .Where(ct => ct.CategoryName != "ALL")
                    .ToList(),
                Confirm = confirm.GetValueOrDefault()
            };

            return View(VM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCar(EditCarViewModel model, HttpPostedFileBase file)
        {
            if(model.Car.CarId > 0) {
                model.Car.IsAvailable = true;
                db.Entry(model.Car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddCar", new { confirm = true });
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    if(ModelState.IsValid) 
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;
                        var path = Path.Combine(Server.MapPath(@"~/Content/Cars/"), filename);

                        file.SaveAs(path);
                        model.Car.PhotoUrl = filename;
                        model.Car.DateAdded = DateTime.Now;
                        model.Car.IsAvailable = true;
                        db.Entry(model.Car).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("AddCar", new { confirm = true });
                    }
                    else
                    {
                        var categories = db.Categories
                            .Where(ct => ct.CategoryName != "ALL")
                            .ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var categories = db.Categories
                        .Where(ct => ct.CategoryName != "ALL")
                        .ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult HideCar(int CarId)
        {
            var car = db.Cars.Find(CarId);
            car.IsAvailable = !car.IsAvailable;
            db.SaveChanges();

            return RedirectToAction("List", "Car");
        }
    }
}