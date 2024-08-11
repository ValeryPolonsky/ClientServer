using Client.Server.Common.Dtos;
using Client.Server.Common.Requests;
using Client.Server.Common.Responses;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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

        public async Task<List<OrderDto>> GetOrders(DateTime fromDate, DateTime toDate, string companyName)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(new GetOrdersRequest
            {
                FromDate = fromDate,
                ToDate = toDate,
                CompanyName = companyName
            });
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"{serverUrl}/home/getorders", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var getOrdersResponse = JsonConvert.DeserializeObject<GetOrdersResponse>(responseJson);

                return getOrdersResponse?.OrderDtos ?? new List<OrderDto>();
            }
            else
                return new List<OrderDto>(); 
        }
    }
}
