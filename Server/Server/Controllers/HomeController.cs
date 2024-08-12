using Client.Server.Common.Dtos;
using Client.Server.Common.Requests;
using Client.Server.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Northwind.Infrastructure;
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

        [HttpPost]
        [Route("/[controller]/GetOrders")]
        public async Task<ActionResult<GetOrdersResponse>> GetOrders([FromBody] GetOrdersRequest request)
        {
            var orders = await NorthwindAccessLayer.Instance.GetOrders(request.FromDate, request.ToDate, request.CompanyName);
            var customers = await NorthwindAccessLayer.Instance.GetCustomers();
            var customersById = new Dictionary<string, Customer>();
            var orderDtos = new List<OrderDto>();
            customers.ForEach(c => 
            {
                customersById[c.CustomerId] = c;
            });
            
            orders.ForEach(o => orderDtos.Add(new OrderDto
            {
                Id = o.OrderId,
                CompanyName = customersById[o.CustomerId].CompanyName,
                ContactName = customersById[o.CustomerId].ContactName,
                OrderDate = o.OrderDate,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress
            }));

            return Ok(new GetOrdersResponse
            {
                OrderDtos = orderDtos
            });
        }
    }
}
