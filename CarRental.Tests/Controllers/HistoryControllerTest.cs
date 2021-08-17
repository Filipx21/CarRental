using CarRental.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Tests.Controllers
{
    [TestClass]
    class HistoryControllerTest
    {
        


        [TestMethod]
        public void Index()
        {
            HistoryController controller = new HistoryController();
            ViewResult result = controller.Index(1) as ViewResult;

            Assert.IsNotNull(result);
        }




       

    }


}
