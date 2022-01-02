using EcommShop.Contracts.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommShop.Business.Business.Interface
{
    public interface IUserRepository
    {
        Task<UserInfoDto> getUserByIdAsync(int id);
        Task<UserInfoDto> addUserAsync(UserCreateDto addObj);
        Task<bool> updateUserAsync(UserInfoDto updateObj);
        Task<bool> deleteUserByIdAsync(int id);
        Task<bool> changePasswordAsync(string username, string oldPassword, string newPassword);
        Task<bool> deActivateUserByIdAsync(int userId);
        Task<bool> activateUserByIdAsync(int userId);
    }
}
