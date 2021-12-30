using EcommShop.Contracts.Dtos.User;
using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AuthenticateUser ([FromForm] UserLoginDto logInDto)
        {
            if (!ModelState.IsValid) return BadRequest("must right UserLogInDto form");
            try
            {
                var access_token = await _authRepository.AuthenticateUser(logInDto.userName, logInDto.userPassword);
                return Ok(new { access_token });
            }catch
            {
                return BadRequest("User not exist or wrong password");
            }
        }
    }
}
