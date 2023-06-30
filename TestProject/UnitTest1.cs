using JukeBoxPartyWeb.Controllers;
using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace TestProject
{
    public class UnitTest1
    {
        public class HomeControllerTests
        {
            [Fact]
            public void Index_ReturnsViewResult()
            {
                var controller = new HomeController();
                var result = controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }

        

    }
}