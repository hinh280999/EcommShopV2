using EcommShop.Contracts.Dtos.User;
using EcommShopVer2.CustomerSite.Services.ApiClient.Interface;
using EcommShopVer2.CustomerSite.Services.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommShopVer2.CustomerSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthApiClient _authApiClient;
        public AccountController(IAuthApiClient authApiClient)
        {
            _authApiClient = authApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> SignInAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(UserLoginDto LogInDto)
        {
            var jwtToken = await _authApiClient.authenticateUser(LogInDto);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            var ClaimsPrincipal = JwtTokenHelpers.exactClaimsPrincipal(jwtToken);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ClaimsPrincipal, authProperties);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string a)
        {
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
