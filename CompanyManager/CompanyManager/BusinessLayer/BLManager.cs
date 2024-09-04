using Client;
using CompanyManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyManager.BusinessLayer
{
    public sealed class BLManager
    {
        private static readonly Lazy<BLManager> _lazyInit = new Lazy<BLManager>(() => new BLManager());
        public static BLManager Instance => _lazyInit.Value;

        private BLManager()
        {
            Orders = new ObservableCollection<OrderModel>();
            LoadData();
        }

        public ObservableCollection<OrderModel> Orders { get; private set; }

        public async Task FilterOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            var orders = await GetOrders(fromDate, toDate, companyName);
            Orders.Clear();
            orders.ForEach(o => Orders.Add(o));
        }

        public async Task<List<ProductModel>> GetProducts(int orderId)
        {
            var productDtos = await ApiConsumer.Instance.GetProducts(orderId);
            var products = new List<ProductModel>();
            productDtos.ForEach(p => products.Add(new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity
            }));

            return products;
        }

        private async void LoadData()
        {
            await FilterOrders(new DateTime(1900, 1, 1), new DateTime(2100, 1, 1), string.Empty);
        }

        private async Task<List<OrderModel>> GetOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            var orderDtos = await ApiConsumer.Instance.GetOrders(fromDate, toDate, companyName);
            var orderModels = new List<OrderModel>();
            orderDtos.ForEach(o => orderModels.Add(new OrderModel 
            {
                Id = o.Id,
                CompanyName = o.CompanyName,
                ContactName = o.ContactName,
                OrderDate = o.OrderDate,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
            }));

            return orderModels;
        }      
    }
}
