using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CompanyManager.BusinessLayer;

namespace CompanyManager.Model
{
    public class OrderModel : ObservableObject
    {
        private int id;
        public int Id 
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string? companyName;
        public string? CompanyName
        {
            get => companyName;
            set => SetProperty(ref companyName, value);
        }

        private string? contactName;
        public string? ContactName
        {
            get => contactName;
            set => SetProperty(ref contactName, value);
        }

        private DateTime? orderDate;
        public DateTime? OrderDate
        {
            get => orderDate;
            set => SetProperty(ref orderDate, value);
        }

        private string? shipName;
        public string? ShipName
        {
            get => shipName;
            set => SetProperty(ref shipName, value);
        }

        private string? shipAddress;
        public string? ShipAddress
        {
            get => shipAddress;
            set => SetProperty(ref shipAddress, value);
        }

        private ObservableCollection<ProductModel>? products;
        public ObservableCollection<ProductModel>? Products 
        {
            get
            {
                if (products == null)
                    FillProducts();

                return products;
            }
            private set => SetProperty(ref products, value);
        }

        private async void FillProducts()
        {            
            var products = await BLManager.Instance.GetProducts(Id);
            Application.Current.Dispatcher.Invoke(() => 
            {
                Products = new ObservableCollection<ProductModel>();
                products.ForEach(p =>
                {
                    Products.Add(p);
                });
            });
        }
    }
}
