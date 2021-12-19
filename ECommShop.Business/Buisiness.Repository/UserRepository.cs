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

        public async Task<UserInfoDto> addUserAsync(UserInfoDto addObj)
        {
            if (await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == addObj.Username) != null)
                throw new Exception("Username already exist in database");

            User addEntity = _mapper.Map<User>(addObj);
            addObj.CreatedTime = DateTime.Now;
            addObj.UpdatedTime = DateTime.Now;

            await _dbContext.Users.AddAsync(addEntity);
            await _dbContext.SaveChangesAsync();

            addObj.Id = addEntity.Id;
            addObj.CreatedTime = addEntity.CreatedTime;
            addObj.UpdatedTime = addEntity.UpdatedTime;

            return addObj;
        }

        public Task<bool> deleteUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto> getUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> updateUserAsync(UserInfoDto updateObj)
        {
            throw new NotImplementedException();
        }
    }
}
