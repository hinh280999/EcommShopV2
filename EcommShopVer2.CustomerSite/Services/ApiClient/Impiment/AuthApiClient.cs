using EcommShop.Contracts.Dtos.User;
using EcommShopVer2.CustomerSite.Services.ApiClient.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcommShopVer2.CustomerSite.Services.ApiClient.Impiment
{
    public class AuthApiClient : IAuthApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> authenticateUser(UserLoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient("EcommBeApiClient");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Authen/authenticateUser", httpContent);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
