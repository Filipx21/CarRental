using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Models;
using System.Data.Entity;
using Moq;
using CarRental.DAL;
using System.Web.Mvc;

namespace CarRental.Controllers.Tests
{
    [TestClass()]
    public class HistoryControllerTests
    {

        [TestMethod]
        public void Index()
        {
            var data = new List<Order>
            {
                new Order() { CarId=1, UserId="1", OrderStatus=OrderStatus.Realized, StartMileage=123, StopMileage=321, TotalCost=432m, CustomerName="das", CustomerAddress="qwe" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CarRentalContext>();
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            HistoryController controller = new HistoryController(mockContext.Object);

            ViewResult result = controller.Index(1) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}