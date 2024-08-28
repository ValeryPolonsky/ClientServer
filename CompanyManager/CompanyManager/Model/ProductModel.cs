using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Model
{
    public class ProductModel : ObservableObject
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string? name;
        public string? Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private decimal? unitPrice;
        public decimal? UnitPrice
        {
            get => unitPrice;
            set => SetProperty(ref unitPrice, value);
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
    }
}
