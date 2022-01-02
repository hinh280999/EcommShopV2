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
        private UsersController usersController;

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
            usersController = new UsersController(_userRepository);


        }

        [Fact]
        public async Task Add_NewUSer_Success()
        {
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

        [Fact]
        public async Task Add_NewUSer_Fail_UsernameExist()
        {
            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.AddUser(userCreateDto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = Assert.IsType<string>(badRequest.Value);

            Assert.NotNull(errorMessage);
        }

        [Fact]
        public async Task Get_User_ById_Success()
        {
            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = true;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.GetUserById(dataUser.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userInfoResult = Assert.IsType<UserInfoDto>(okResult.Value);

            Assert.NotNull(userInfoResult);
            Assert.Equal(userInfoResult.Username, dataUser.Username);
        }

        [Fact]
        public async Task Get_User_ById_Fail_NotFound_Id()
        {
            int notFoundId = 9999;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = true;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.GetUserById(notFoundId);

            var notFoundRusult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_User_ById_Fail_Not_Active()
        {
            bool isNotActive = false;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isNotActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.GetUserById(dataUser.Id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ActivateUser_Success()
        {
            bool isNotActive = false;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isNotActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.ActivateUser(dataUser.Id);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var activeResult = Assert.IsType<bool>(okResult.Value);

            Assert.True(activeResult);
        }

        [Fact]
        public async Task ActivateUser_Fail_NotFoundID_Active()
        {
            bool isActive = true;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.ActivateUser(dataUser.Id);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeActivateUser_Success()
        {
            bool isActive = true;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.DeActivateUser(dataUser.Id);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var activeResult = Assert.IsType<bool>(okResult.Value);

            Assert.True(activeResult);
        }

        [Fact]
        public async Task DeActivateUser_Fail_NotFoundID_Nott_Active()
        {
            bool isActive = false;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.DeActivateUser(dataUser.Id);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Change_User_Password_Success()
        {
            string newPassword = "newPassword";

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = true;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.ChangePassword("Admin001", "myPassword001", newPassword);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userData = Assert.IsType<bool>(okResult.Value);

            Assert.True(userData);
        }

        [Fact]
        public async Task Change_User_Password_Fail_NotFound_User()
        {
            string notFound_UserName = "name";
            bool isActive = false;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.ChangePassword(notFound_UserName, "myPassword001", "newPassword");
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Change_User_Password_Fail_OldPassword_Not_Match()
        {
            string wrongOldPassword = "wrongPassword";
            bool isActive = true;

            UserCreateDto userCreateDto = new UserCreateDto();
            userCreateDto.FirstName = "FirstName";
            userCreateDto.LastName = "LastName";
            userCreateDto.Email = "Email@gmail.com";
            userCreateDto.PhoneNumber = "0981986252";
            userCreateDto.Addresss = "Address";
            userCreateDto.Username = "Admin001";
            userCreateDto.Password = "myPassword001";
            userCreateDto.UserType = UserType.ADMIN;

            User dataUser = _mapper.Map<User>(userCreateDto);
            dataUser.Active = isActive;

            await _dbContext.Users.AddAsync(dataUser);
            await _dbContext.SaveChangesAsync();

            var result = await usersController.ChangePassword("Admin001", wrongOldPassword, "newPassword");
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }
    }
}
