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
    public class CompanyManagerViewModel
    {
        public CompanyManagerViewModel()
        {
            var orders = BLManager.Instance.Orders;
        }

        public ObservableCollection<OrderModel> Orders => BLManager.Instance.Orders;
    }
}
