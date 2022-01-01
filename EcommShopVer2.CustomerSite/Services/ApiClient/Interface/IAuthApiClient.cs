using EcommShop.Contracts.Dtos.User;
using System;
using System.Threading.Tasks;

namespace EcommShopVer2.CustomerSite.Services.ApiClient.Interface
{
    public interface IAuthApiClient
    {
        Task<String> authenticateUser(UserLoginDto loginDto);
    }
}
