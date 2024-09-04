using Microsoft.EntityFrameworkCore;
using Northwind.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Infrastructure
{
    public class NorthwindAccessLayer : INorthwindAccessLayer
    {
        private INorthwindContext _context;

        public NorthwindAccessLayer(INorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate)
        {
            var orders = await _context.Orders.Where(o => o.OrderDate >= fromDate &&
                                                    o.OrderDate <= toDate)
                                        .ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            var customers = await _context.Customers.Where(c => c.CompanyName.ToLower().Contains(companyName.ToLower()))
                                                .ToListAsync();
            var customerIds = customers.Select(c => c.CustomerId)
                                        .ToHashSet();
            var orders = await _context.Orders.Where(o => customerIds.Contains(o.CustomerId) &&
                                                    o.OrderDate >= fromDate &&
                                                    o.OrderDate <= toDate)
                                        .ToListAsync();
            return orders;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<List<Product>> GetProducts(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId)
                .ToListAsync();
            var productIds = orderDetails.Select(od => od.ProductId)
                .ToHashSet();
            var products = await _context.Products.Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            return products;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId)
                .ToListAsync();

            return orderDetails;
        }
    }
}
