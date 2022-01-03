using AutoMapper;
using EcommShop.Contracts.Dtos.Brand;
using EcommShop.DataAccessor.DBContext;
using EcommShop.DataAccessor.Entities;
using ECommShop.Business;
using ECommShop.Business.Buisiness.Repository;
using EcomShopVers2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommShopVer2.IntegrationTests.ControllerShould
{
    public class BrandControllerShould
    {
        private BrandRepository _brandRepository;
        private BrandsController _brandsController;
        private readonly IMapper _mapper;
        private SqliteConnection _connection;
        private EcommShopDBContext _dbContext;

        public BrandControllerShould()
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

            _brandRepository = new BrandRepository(_dbContext, _mapper);
            _brandsController = new BrandsController(_brandRepository);
        }

        [Fact]
        public async Task add_Brand_Sucess()
        {
            string brandName = "New Brand";

            var result = await _brandsController.CreateBrandAsync(brandName);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var brandInfoResult = Assert.IsType<BrandInfoDto>(okResult.Value);

            Assert.NotNull(brandInfoResult);
            Assert.Equal(brandInfoResult.Name, brandName);
        }

        [Fact]
        public async Task add_Brand_Fail_ExistBrandName()
        {
            string existBrandName = "ExistBrand";

            var brandDb = new Brand();
            brandDb.Name = "ExistBrand";
            brandDb.Active = true;
            brandDb.CreatedTime = DateTime.Now;
            brandDb.UpdatedTime = DateTime.Now;

            await _dbContext.Brands.AddAsync(brandDb);
            await _dbContext.SaveChangesAsync();

            var result = await _brandsController.CreateBrandAsync(existBrandName);
            var badReqResult = Assert.IsType<BadRequestObjectResult>(result);
            var brandInfoResult = Assert.IsType<string>(badReqResult.Value);

            Assert.NotNull(brandInfoResult);
        }

    }
}
