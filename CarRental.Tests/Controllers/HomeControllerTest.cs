using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental;
using CarRental.Controllers;

namespace CarRental.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckStaticPagesTest()
        {
            var controller = new HomeController();
            var result = controller.StronyStatyczne("Kontakt") as ViewResult;
            Assert.AreEqual("Kontakt", result.ViewName);
        }

    }
}
