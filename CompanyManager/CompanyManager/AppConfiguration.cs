using Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager
{
    public class AppConfiguration
    {
        private static readonly Lazy<AppConfiguration> lazy = new Lazy<AppConfiguration>(() => new AppConfiguration());
        public static AppConfiguration Instance => lazy.Value;

        private AppConfiguration()
        {      
            ServerUrl = string.Empty;
        }

        public void Load()
        {
            var appConfiguration = LoadConfiguration();
            ServerUrl = appConfiguration.ServerUrl;
        }

        private AppConfiguration LoadConfiguration()
        {
            UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().Location);
            string tempFolder = Uri.UnescapeDataString(uri.Path);
            string appAssemblyFolder = Path.GetDirectoryName(tempFolder);

            string configurationFilePath = Path.Combine(appAssemblyFolder, "appsettings.json");
            if (File.Exists(configurationFilePath) == false)
                throw new Exception($"App configuration file {configurationFilePath} was not found.");

            string appConfigurationJson = File.ReadAllText(configurationFilePath);
            if (string.IsNullOrEmpty(appConfigurationJson))
                throw new Exception($"App configuration file is empty.");

            return JsonConvert.DeserializeObject<AppConfiguration>(appConfigurationJson);
        }

        public string ServerUrl { get; set; }
    }
}
