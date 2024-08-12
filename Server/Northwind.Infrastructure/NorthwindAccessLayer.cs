using Microsoft.EntityFrameworkCore;
using Northwind.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Infrastructure
{
    public class NorthwindAccessLayer
    {
        private static readonly Lazy<NorthwindAccessLayer> lazyInit = new Lazy<NorthwindAccessLayer>(() => new NorthwindAccessLayer());
        public static NorthwindAccessLayer Instance => lazyInit.Value;

        private NorthwindAccessLayer()
        {
        }

        public async Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate)
        {
            using (var db = new NorthwindContext())
            {
                var orders = await db.Orders.Where(o => o.OrderDate >= fromDate &&
                                                        o.OrderDate <= toDate)
                                            .ToListAsync();
                return orders;               
            }
        }

        public async Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            using (var db = new NorthwindContext())
            {
                var customers = await db.Customers.Where(c => c.CompanyName.ToLower().Contains(companyName.ToLower()))
                                                  .ToListAsync();
                var customerIds = customers.Select(c => c.CustomerId)
                                           .ToHashSet();
                var orders = await db.Orders.Where(o => customerIds.Contains(o.CustomerId) &&
                                                        o.OrderDate >= fromDate && 
                                                        o.OrderDate <= toDate)
                                            .ToListAsync();
                return orders;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            using (var db = new NorthwindContext())
            {
                var customers = await db.Customers.ToListAsync();
                return customers;
            }
        }
    }
}
