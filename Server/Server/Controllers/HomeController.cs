using Microsoft.AspNetCore.Mvc;
using Northwind.Infrastructure.Models;
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
            var context = new NorthwindContext();
            var orders = context.Orders.ToList();
            return $"Number of orders: {orders.Count}";
        }
    }
}
