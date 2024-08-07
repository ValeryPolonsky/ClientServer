using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    public sealed class ApiConsumer
    {
        private string serverUrl;
        private static readonly Lazy<ApiConsumer> lazy = new Lazy<ApiConsumer>(() => new ApiConsumer());
        public static ApiConsumer Instance => lazy.Value; 

        private ApiConsumer()
        {
            serverUrl = "https://localhost:7180";
        }    
        
        public async Task<string> GetData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{serverUrl}/home/getdata");
            var data = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var content= await response.Content.ReadAsStringAsync();
                data = content;
            }
            
            return data;
        }    
    }
}
