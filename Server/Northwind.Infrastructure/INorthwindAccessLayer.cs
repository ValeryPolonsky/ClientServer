using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure
{
    public interface INorthwindAccessLayer
    {
        Task<List<Customer>> GetCustomers();
        Task<List<OrderDetail>> GetOrderDetails(int orderId);
        Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate);
        Task<List<Order>> GetOrders(DateTime fromDate, DateTime toDate, string companyName);
        Task<List<Product>> GetProducts(int orderId);
    }
}