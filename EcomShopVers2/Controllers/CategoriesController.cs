using EcommShop.Contracts.Dtos.Category;
using ECommShop.Business.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcomShopVers2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var listCategory = await _categoryRepository.getAllCategoriesAsync();
            return Ok(listCategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.getCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            try
            {
                var categoryInfo = await _categoryRepository.createCategoryAsync(categoryName);
                return Ok(categoryInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryUpdateDto updateDto)
        {
            try
            {
                var categoryInfo = await _categoryRepository.updateCategoryAsync(id, updateDto);
                return Ok(categoryInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryRepository.deleteCategoryAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
