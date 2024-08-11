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
            var orders = await NorthwindAccessLayer.Instance.GetOrders(request.FromDate, request.ToDate);
            var orderDtos = new List<OrderDto>();
            orders.ForEach(o => orderDtos.Add(new OrderDto 
            { 
                Id = o.OrderId,
                CompanyName = "",
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
