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
        private INorthwindAccessLayer _northwindAccessLayer;

        public HomeController(ILogger<HomeController> logger,
            INorthwindAccessLayer northwindAccessLayer)
        {
            _logger = logger;
            _northwindAccessLayer = northwindAccessLayer;
        }        

        [HttpPost]
        [Route("/[controller]/GetOrders")]
        public async Task<ActionResult<GetOrdersResponse>> GetOrders([FromBody] GetOrdersRequest request)
        {
            var orders = await _northwindAccessLayer.GetOrders(request.FromDate, request.ToDate, request.CompanyName);
            var customers = await _northwindAccessLayer.GetCustomers();
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
            var products = await _northwindAccessLayer.GetProducts(request.OrderId);
            var orderDetails = await _northwindAccessLayer.GetOrderDetails(request.OrderId);
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
