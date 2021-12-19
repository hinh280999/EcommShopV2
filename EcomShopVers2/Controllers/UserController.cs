using EcommShop.Contracts.Dtos.User;
using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost()]
        public async Task<ActionResult> AddUser(UserInfoDto addUserDto)
        {
            try{
                var result = await _userRepository.addUserAsync(addUserDto);
                return Ok(result);
            }
            catch {
                return BadRequest();
            }
        }
    }
}
