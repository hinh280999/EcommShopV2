using EcommShop.Contracts.Dtos.User;
using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost()]
        public async Task<ActionResult> AddUser(UserCreateDto addUserDto)
        {
            try {
                var result = await _userRepository.addUserAsync(addUserDto);
                return Ok(result);
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var result = await _userRepository.getUserByIdAsync(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserById(int id, UserInfoDto updateObj)
        {
            try
            {
                updateObj.Id = id;
                var result = await _userRepository.updateUserAsync(updateObj);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("/api/deactivate-user/{id}")]
        public async Task<ActionResult> DeActivateUser(int id)
        {
            try
            {
                var result = await _userRepository.deActivateUserByIdAsync(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("/api/activate-user/{id}")]
        public async Task<ActionResult> ActivateUser(int id)
        {
            try
            {
                var result = await _userRepository.activateUserByIdAsync(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("/api/change-password/{username}")]
        public async Task<ActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                var result = await _userRepository.changePasswordAsync(username, oldPassword,newPassword);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
