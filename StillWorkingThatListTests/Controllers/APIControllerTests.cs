using Microsoft.VisualStudio.TestTools.UnitTesting;
using StillWorkingThatList.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StillWorkingThatList.Models;
using System.Web;
using System.Web.Http;

namespace StillWorkingThatList.Controllers.Tests
{
    [TestClass()]
    public class APIControllerTests
    {
        [TestMethod()]
        public void GetJobjectTest()
        {
            APIController controller = new APIController();

            string Name = "arya%20stark";

            var result = controller.GetCharacter(Name);


            Assert.IsNotNull(result);
        }
    }
}