using AutoMapper;
using EcommShop.Contracts.Dtos.User;
using EcommShop.DataAccessor.DBContext;
using EcommShop.DataAccessor.Entities;
using ECommShop.Business;
using ECommShop.Business.Buisiness.Repository;
using EcommShopVer2.IntegrationTests.Common;
using EcomShopVers2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EcommShopVer2.IntegrationTests.ControllerShould
{
    public class UserControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private UserRepository _userRepository;
        private readonly IMapper _mapper;
        private SqliteConnection _connection;
        private EcommShopDBContext _dbContext;

        public UserControllerShould()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<EcommShopDBContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new EcommShopDBContext(options);
            _dbContext.Database.EnsureCreated();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userRepository = new UserRepository(_dbContext, _mapper);


        }

        [Fact]
        public async Task Add_NewUSer_Success()
        {
            var usersController = new UsersController(_userRepository);

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            var result = await usersController.AddUser(userCreateDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userInfoResult = Assert.IsType<UserInfoDto>(okResult.Value);

            Assert.NotNull(userInfoResult);
            Assert.Equal(userCreateDto.Email, userInfoResult.Email);
        }
    }
}
