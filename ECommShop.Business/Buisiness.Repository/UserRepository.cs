using AutoMapper;
using EcommShop.Contracts.Dtos.User;
using EcommShop.DataAccessor.DBContext;
using EcommShop.DataAccessor.Entities;
using ECommShop.Business.Business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommShop.Business.Buisiness.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommShopDBContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(EcommShopDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> addUserAsync(UserCreateDto addObjDti)
        {
            var userinDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == addObjDti.Username);
            if (userinDb != null) 
                throw new Exception("Username already exist in database");

            User addEntity = _mapper.Map<User>(addObjDti);

            addEntity.CreatedTime = DateTime.Now;
            addEntity.UpdatedTime = DateTime.Now;
            addEntity.Active = true;

            await _dbContext.Users.AddAsync(addEntity);
            await _dbContext.SaveChangesAsync();

            UserInfoDto userInfo = _mapper.Map<UserInfoDto>(addEntity);
            return userInfo;
        }

        public async Task<bool> deleteUserByIdAsync(int id)
        {
            var userInDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userInDb == null)
                throw new Exception("Cant find user with id : " + id);
            userInDb.Active = false;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserInfoDto> getUserByIdAsync(int id)
        {
            var userInDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (userInDb == null)
                throw new Exception("Cant find user with id : " + id);

            UserInfoDto userInfoDto = _mapper.Map<UserInfoDto>(userInDb);
            return userInfoDto;
        }

        public async Task<bool> updateUserAsync(UserInfoDto updateObj)
        {
            var userInDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == updateObj.Id && x.Active == true);

            if (userInDb == null)
                throw new Exception("Cant find user with id : " + updateObj.Id);

            userInDb.FirstName = updateObj.FirstName;
            userInDb.LastName = updateObj.LastName;
            userInDb.PhoneNumber = updateObj.PhoneNumber;
            userInDb.UpdatedTime = DateTime.Now;
            userInDb.Email = updateObj.Email;
            userInDb.Addresss = updateObj.Addresss;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> changePasswordAsync (string username, string oldPassword, string newPassword)
        {
            var userInDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Active == true);

            if (userInDb == null)
                throw new Exception("Cant find user with username : " + username);
            if (userInDb.Password != oldPassword)
                throw new Exception("Old Password not match : " + oldPassword);

            userInDb.Password = newPassword;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
