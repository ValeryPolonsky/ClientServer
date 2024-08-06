using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }        

        [HttpGet]
        [Route("/[controller]/GetData")]
        public string GetData()
        {
            return "Test";
        }
    }
}
