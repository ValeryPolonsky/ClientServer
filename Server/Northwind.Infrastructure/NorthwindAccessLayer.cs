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
    }
}
