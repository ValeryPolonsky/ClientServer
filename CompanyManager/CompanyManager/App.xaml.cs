using CompanyManager.View;
using CompanyManager.ViewModel;
using System.Configuration;
using System.Data;
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
            var companyManagerVM = new CompanyManagerViewModel();
            var companyManagerWindow = new CompanyManagerWindow();
            companyManagerWindow.DataContext = companyManagerVM;
            companyManagerWindow.Show();
        }
    }

}
