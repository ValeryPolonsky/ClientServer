using Client;
using CompanyManager.View;
using CompanyManager.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace CompanyManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppConfiguration.Instance.Load();
            ApiConsumer.Instance.Init(AppConfiguration.Instance.ServerUrl);

            var companyManagerVM = new CompanyManagerViewModel();
            var companyManagerWindow = new CompanyManagerWindow();
            companyManagerWindow.DataContext = companyManagerVM;
            companyManagerWindow.Show();
        }       
    }

}
