using EcommShop.Contracts.Dtos.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommShop.Business.Business.Interface
{
    public interface IBrandRepository
    {
        Task<List<BrandInfoDto>> getAllBrandsAsync();
        Task<BrandInfoDto> getBrandByIdAsync(int id);
        Task<BrandInfoDto> createBrandAsync(string brandName);
        Task<BrandInfoDto> updateBrandAsync(int id, string name);
        Task<bool> deleteBrandAsync(int id);
    }
}
