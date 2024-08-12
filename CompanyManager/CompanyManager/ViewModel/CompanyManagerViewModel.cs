using CommunityToolkit.Mvvm.ComponentModel;
using CompanyManager.BusinessLayer;
using CompanyManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.ViewModel
{
    public class CompanyManagerViewModel : ObservableObject
    {
        public CompanyManagerViewModel()
        {
        }

        public ObservableCollection<OrderModel> Orders => BLManager.Instance.Orders;
    }
}
