using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthenController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost("authenticateUser")]
        public async Task<IActionResult> AuthenticateUser (string username, string password)
        {
            try
            {
                var access_token = await _authRepository.AuthenticateUser(username, password);
                return Ok(new { access_token });
            }catch (Exception e)
            {
                return BadRequest("User not exist or wrong password");
            }
        }
    }
}
