using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            FilterCommand = new RelayCommand(FilterCommandExecute, FilterCommandCanExecute);
        }

        public ObservableCollection<OrderModel> Orders => BLManager.Instance.Orders;

        private DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }

        public RelayCommand FilterCommand { get; set; }
        private async void FilterCommandExecute()
        {
            
        }
        private bool FilterCommandCanExecute() => true;       
    }
}
