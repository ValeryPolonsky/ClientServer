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
        private bool filteringOrders;
        private readonly DateTime minDate = new DateTime(1900, 1, 1);

        public CompanyManagerViewModel()
        {
            FilterCommand = new RelayCommand(FilterCommandExecute, FilterCommandCanExecute);
            FromDate = minDate;
            ToDate = DateTime.Now;
            CompanyName = string.Empty;
        }

        public ObservableCollection<OrderModel> Orders => BLManager.Instance.Orders;

        private DateTime? fromDate;
        public DateTime? FromDate
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }

        private DateTime? toDate;
        public DateTime? ToDate
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }

        private string? companyName;
        public string? CompanyName
        {
            get => companyName;
            set => SetProperty(ref companyName, value);
        }

        public RelayCommand FilterCommand { get; set; }
        private async void FilterCommandExecute()
        {
            filteringOrders = true;
            FilterCommand.NotifyCanExecuteChanged();
            await BLManager.Instance.FilterOrders(FromDate ?? minDate, ToDate ?? DateTime.Now, CompanyName ?? string.Empty);
            filteringOrders = false;
            FilterCommand.NotifyCanExecuteChanged();
        }
        private bool FilterCommandCanExecute()
        {
            if (filteringOrders)
                return false;

            return true;
        }        
    }
}
