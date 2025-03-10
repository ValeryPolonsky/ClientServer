﻿using Client.Server.Common.Dtos;
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
        private string _serverUrl;
        private static readonly Lazy<ApiConsumer> _lazyInit = new Lazy<ApiConsumer>(() => new ApiConsumer());
        public static ApiConsumer Instance => _lazyInit.Value; 

        private ApiConsumer()
        {
            _serverUrl = string.Empty;
        }   
        
        public void Init(string serverUrl)
        {
            _serverUrl = serverUrl;
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
            
            var response = await client.PostAsync($"{_serverUrl}/Home/GetOrders", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var getOrdersResponse = JsonConvert.DeserializeObject<GetOrdersResponse>(responseJson);

                return getOrdersResponse?.OrderDtos ?? new List<OrderDto>();
            }
            else
                return new List<OrderDto>(); 
        }

        public async Task<List<ProductDto>> GetProducts(int orderId)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(new GetProductsRequest
            {
                OrderId = orderId
            });
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_serverUrl}/Home/GetProducts", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var getOrdersResponse = JsonConvert.DeserializeObject<GetProductsResponse>(responseJson);

                return getOrdersResponse?.ProductDtos ?? new List<ProductDto>();
            }
            else
                return new List<ProductDto>();
        }
    }
}
