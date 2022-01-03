using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandRepository.getAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
           
           var brandInfo = await _brandRepository.getBrandByIdAsync(id);
            return Ok(brandInfo);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBrandAsync(string brandName)
        {
            try
            {
                var brandInfo = await _brandRepository.createBrandAsync(brandName);
                return Ok(brandInfo);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(int id, string brandName)
        {
            try
            {
                var brandInfo = await _brandRepository.updateBrandAsync(id, brandName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            try
            {
                var brandInfo = await _brandRepository.deleteBrandAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
