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

        [HttpPost]
        [Route("/[controller]/GetProducts")]
        public async Task<ActionResult<GetProductsResponse>>GetProducts([FromBody] GetProductsRequest request)
        {
            var products = await NorthwindAccessLayer.Instance.GetProducts(request.OrderId);
            var orderDetails = await NorthwindAccessLayer.Instance.GetOrderDetails(request.OrderId);
            var productDtos = new List<ProductDto>();

            products.ForEach(p => 
            {
                productDtos.Add(new ProductDto 
                { 
                    Id = p.ProductId,
                    Name = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    Quantity = orderDetails.First(od => od.OrderId == request.OrderId && 
                        od.ProductId == p.ProductId).Quantity
                });
            });

            return Ok(new GetProductsResponse 
            { 
                ProductDtos = productDtos
            });
        }
    }
}
