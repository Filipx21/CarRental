using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarRental.DAL;
using System.Data.Entity;
using CarRental.Models;
using System.Web.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace CarRental.Controllers.Tests
{
    [TestClass()]
    public class CarControllerTests
    {
        [TestMethod()]
        public void ListAllCarsTest()
        {
            var mockContext = new Mock<CarRentalContext>();
            mockContext.Setup(c => c.Cars).Returns(PrepareCarsData().Object);

            var controller = new CarController(mockContext.Object);
            controller.ControllerContext = PrepareControllerContext().Object;

            var result = controller.List("ALL") as ViewResult;

            var viewModel = result.ViewData.Model as IEnumerable<Car>;

            Assert.IsTrue(viewModel.Count() == 16);
        }

        [TestMethod()]
        public void CategoriesMenuTest()
        {
            var mockContext = new Mock<CarRentalContext>();
            mockContext.Setup(c => c.Categories).Returns(PrepareCategoriesData().Object);

            var controller = new CarController(mockContext.Object);
            var result = controller.CategoriesMenu() as PartialViewResult;

            var viewModel = result.ViewData.Model as IEnumerable<Category>;

            Assert.IsTrue(viewModel.Count() == 9);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var carId = 1;
            var mockContext = new Mock<CarRentalContext>();
            mockContext.Setup(c => c.Categories).Returns(PrepareCategoriesData().Object);
            mockContext.Setup(c => c.Cars).Returns(PrepareCarsData().Object);

            var controller = new CarController(mockContext.Object);

            var result = controller.Details(carId) as ViewResult;

            var viewModel = result.ViewData.Model as Car;

            Assert.IsTrue(viewModel.Producer == "Renault");
        }

        [TestMethod()]
        public void FindNameTest()
        {
            var model = "Trafic";
            var mockContext = new Mock<CarRentalContext>();
            mockContext.Setup(c => c.Categories).Returns(PrepareCategoriesData().Object);
            mockContext.Setup(c => c.Cars).Returns(PrepareCarsData().Object);

            var controller = new CarController(mockContext.Object);

            var result = controller.FindName(model) as JsonResult;

            var jsonResult = result.Data.ToString();
 
            Assert.IsNotNull(jsonResult);
        }

        private Mock<DbSet<Car>> PrepareCarsData()
        {
            var data = new List<Car>
            {
                new Car(){CarId = 1, CategoryId = 1, Producer = "Renault", Model = "Trafic", EngineCapacity = 2.0, Mileage = 239000, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "renault-trafic-2-0.png", CostPerDay = 0.13m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 2, CategoryId = 2, Producer = "Opel", Model = "Vectra", EngineCapacity = 1.9, Mileage = 280000, Color = "Zielony", ProductionDate = DateTime.Now, PhotoUrl = "opel-vectra-1-9.png", CostPerDay = 0.10m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 3, CategoryId = 3, Producer = "Opel", Model = "Zafira", EngineCapacity = 1.7, Mileage = 179975, Color = "Szary", ProductionDate = DateTime.Now, PhotoUrl = "opel-zafira-1-9.png", CostPerDay = 0.09m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 4, CategoryId = 4, Producer = "Audi", Model = "A5 Sportback", EngineCapacity = 2.0, Mileage = 193000, Color = "Szary", ProductionDate = DateTime.Now, PhotoUrl = "audi-A5-Sportback-2-0.png", CostPerDay = 0.17m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 5, CategoryId = 5, Producer = "Land Rover", Model = "Discovery Sport", EngineCapacity = 2.0, Mileage = 49000, Color = "Czarny", ProductionDate = DateTime.Now, PhotoUrl = "lr-discovery-sport-2-0.png", CostPerDay = 0.15m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 6, CategoryId = 6, Producer = "Ford", Model = "F150", EngineCapacity = 5.0, Mileage = 78000, Color = "Czarny", ProductionDate = DateTime.Now, PhotoUrl = "ford-f150-5-0.png", CostPerDay = 0.15m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 7, CategoryId = 7, Producer = "Volvo", Model = "FH 500", EngineCapacity = 13.0, Mileage = 616716, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "Volvo-FH-500-13-0.png", CostPerDay = 0.28m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 8, CategoryId = 8, Producer = "Volvo", Model = "FL 6", EngineCapacity = 5.48, Mileage = 546425, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "volvo-FL-6-5-4-8.png", CostPerDay = 0.28m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 9, CategoryId = 1, Producer = "Renault", Model = "Trafic 2", EngineCapacity = 2.0, Mileage = 123322, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "renault-trafic-2-0.png", CostPerDay = 0.13m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 10, CategoryId = 2, Producer = "Opel", Model = "Vectra 2", EngineCapacity = 1.9, Mileage = 49000, Color = "Zielony", ProductionDate = DateTime.Now, PhotoUrl = "opel-vectra-1-9.png", CostPerDay = 0.10m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 11, CategoryId = 3, Producer = "Opel", Model = "Zafira 2", EngineCapacity = 1.7, Mileage = 179975, Color = "Szary", ProductionDate = DateTime.Now, PhotoUrl = "opel-zafira-1-9.png", CostPerDay = 0.09m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 12, CategoryId = 4, Producer = "Audi", Model = "A5 Sportback 2", EngineCapacity = 2.0, Mileage = 193000, Color = "Szary", ProductionDate = DateTime.Now, PhotoUrl = "audi-A5-Sportback-2-0.png", CostPerDay = 0.17m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 13, CategoryId = 5, Producer = "Land Rover", Model = "Discovery Sport 2", EngineCapacity = 2.0, Mileage = 133000, Color = "Czarny", ProductionDate = DateTime.Now, PhotoUrl = "lr-discovery-sport-2-0.png", CostPerDay = 0.15m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 14, CategoryId = 6, Producer = "Ford", Model = "F150 2", EngineCapacity = 5.0, Mileage = 179975, Color = "Czarny", ProductionDate = DateTime.Now, PhotoUrl = "ford-f150-5-0.png", CostPerDay = 0.15m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 15, CategoryId = 7, Producer = "Volvo", Model = "FH 500 2", EngineCapacity = 13.0, Mileage = 616716, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "Volvo-FH-500-13-0.png", CostPerDay = 0.28m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now},
                new Car(){CarId = 16, CategoryId = 8, Producer = "Volvo", Model = "FL 6 2", EngineCapacity = 5.48, Mileage = 546425, Color = "Biały", ProductionDate = DateTime.Now, PhotoUrl = "volvo-FL-6-5-4-8.png", CostPerDay = 0.28m, DateAdded = DateTime.Now, IsAvailable = true, IsUsed = false, LastUpdate = DateTime.Now}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        private Mock<DbSet<Category>> PrepareCategoriesData()
        {
            var data = new List<Category>
            {
                new Category(){CategoryId = 1, CategoryName = "ALL", ShortName = "ALL", CategoryDescription = "Brak1", DateAdded = DateTime.Now},
                new Category(){CategoryId = 2, CategoryName = "VAN", ShortName = "V", CategoryDescription = "Brak1", DateAdded = DateTime.Now},
                new Category(){CategoryId = 3, CategoryName = "KOMBI", ShortName = "K", CategoryDescription = "Brak2", DateAdded = DateTime.Now},
                new Category(){CategoryId = 4, CategoryName = "MINIVAN", ShortName = "M", CategoryDescription = "Brak3", DateAdded = DateTime.Now},
                new Category(){CategoryId = 5, CategoryName = "SEDAN", ShortName = "S", CategoryDescription = "Brak4", DateAdded = DateTime.Now},
                new Category(){CategoryId = 6, CategoryName = "SUV", ShortName = "SUV", CategoryDescription = "Brak5", DateAdded = DateTime.Now},
                new Category(){CategoryId = 7, CategoryName = "PICK UP", ShortName = "PU", CategoryDescription = "Brak6", DateAdded = DateTime.Now},
                new Category(){CategoryId = 8, CategoryName = "Ciągnik Siodłowy", ShortName = "CS", CategoryDescription = "Brak7", DateAdded = DateTime.Now},
                new Category(){CategoryId = 9, CategoryName = "Ciężarowy Specjalny", ShortName = "CS", CategoryDescription = "Brak9", DateAdded = DateTime.Now},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        private Mock<ControllerContext> PrepareControllerContext()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Admin")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns("test");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.Setup(r => r.HttpContext.Request["X-Requested-With"]).Returns("");

            return controllerContext;
        }

    }
}