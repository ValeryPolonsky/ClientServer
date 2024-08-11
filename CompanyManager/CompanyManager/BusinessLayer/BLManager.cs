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
        private static readonly Lazy<BLManager> lazy = new Lazy<BLManager>(() => new BLManager());
        public static BLManager Instance => lazy.Value;

        private BLManager()
        {
            Orders = new ObservableCollection<OrderModel>();
            LoadData();
        }

        public ObservableCollection<OrderModel> Orders { get; set; }

        public async Task<List<OrderModel>> GetOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            var orderDtos = await ApiConsumer.Instance.GetOrders(fromDate, toDate, companyName);
            var orderModels = new List<OrderModel>();
            orderDtos.ForEach(o => orderModels.Add(new OrderModel 
            {
                Id = o.Id,
                CompanyName = o.CompanyName,
                OrderDate = o.OrderDate,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
            }));

            return orderModels;
        }

        private async void LoadData()
        {
            var orders = await GetOrders(new DateTime(1995, 1, 1), new DateTime(2000, 1, 1), "");
            Orders.Clear();
            orders.ForEach(o => Orders.Add(o));
        }
    }
}
