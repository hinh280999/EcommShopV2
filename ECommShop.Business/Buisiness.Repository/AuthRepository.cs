using EcommShop.DataAccessor.DBContext;
using ECommShop.Business.Business.Interface;
using ECommShop.Business.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommShop.Business.Buisiness.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EcommShopDBContext _dbContext;
        private TokenHelpers _tokenHelpers;
        public AuthRepository(EcommShopDBContext context, TokenHelpers tokenHelpers)
        {
            _dbContext = context;
            _tokenHelpers = tokenHelpers;
        }
        public async Task<string> AuthenticateUser(string Username, string password)
        {
            var userInDB = await _dbContext.Users.FirstOrDefaultAsync( x => x.Username == Username && x.Active == true);

            if (userInDB == null) throw new Exception("No user with Username: " + Username);
            if(userInDB.Password != password) throw new Exception("Wrong password : " + Username);

            return _tokenHelpers.GenerateJSONWebToken(userInDB);
        }
    }
}
