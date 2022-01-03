using AutoMapper;
using EcommShop.Contracts.Dtos.Brand;
using EcommShop.DataAccessor.DBContext;
using EcommShop.DataAccessor.Entities;
using ECommShop.Business.Business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommShop.Business.Buisiness.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly EcommShopDBContext _dbContext;
        private readonly IMapper _mapper;

        public BrandRepository(EcommShopDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<BrandInfoDto> createBrandAsync(string brandName)
        {
            var brandInDb = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Name == brandName);
            if (brandInDb != null) throw new Exception("Brand already exist: ");

            var brand = new Brand();
            brand.Name = brandName;
            brand.Active = true;
            brand.CreatedTime = DateTime.Now;
            brand.UpdatedTime = DateTime.Now;

            await _dbContext.Brands.AddAsync(brand);
            await _dbContext.SaveChangesAsync();

            var BrandInfo = _mapper.Map<BrandInfoDto>(brand);
            return BrandInfo;
        }

        public async Task<bool> deleteBrandAsync(int id)
        {
            var ExistBrand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);

            if (ExistBrand == null) throw new Exception("No brand found");

            var productInBrand = await _dbContext.Products.FirstOrDefaultAsync(x => x.BrandId == id);
            if (productInBrand != null) throw new Exception("There is product in this Brand:");

           _dbContext.Brands.Remove(ExistBrand);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<BrandInfoDto>> getAllBrandsAsync()
        {
            var listBrands = await _dbContext.Brands.Where(x => x.Active == true).ToListAsync();

            var listBrandsIinfo = _mapper.Map<List<Brand>, List<BrandInfoDto>>(listBrands);

            return listBrandsIinfo;
        }

        public async Task<BrandInfoDto> getBrandByIdAsync(int id)
        {
            var Brand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
            BrandInfoDto brandInfo = _mapper.Map<BrandInfoDto>(Brand);
            return brandInfo;
        }

        public async Task<BrandInfoDto> updateBrandAsync(int id, string name)
        {
            var ExistBrand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (ExistBrand == null) throw new Exception("No brand found");

            ExistBrand.Name = name;
            ExistBrand.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            var BrandInfo = _mapper.Map<BrandInfoDto>(ExistBrand);
            return BrandInfo;
        }
    }
}
