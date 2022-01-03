using AutoMapper;
using EcommShop.Contracts.Dtos.Category;
using EcommShop.DataAccessor.DBContext;
using EcommShop.DataAccessor.Entities;
using ECommShop.Business.Business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommShop.Business.Buisiness.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommShopDBContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryRepository(EcommShopDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public async Task<CategoryInfoDto> createCategoryAsync(string categoryName)
        {
            var categoryInDb = await _dbContext.Categories.FirstOrDefaultAsync(x => x.categoryName == categoryName);
            if (categoryInDb != null) throw new Exception("Category already exist: ");

            var category = new Category();
            category.categoryName = categoryName;
            category.Active = true;
            category.CreatedTime = DateTime.Now;
            category.UpdatedTime = DateTime.Now;

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var categoryInfo = _mapper.Map<CategoryInfoDto>(category);

            return categoryInfo;
        }

        public async Task<bool> deleteCategoryAsync(int id)
        {
            var ExistCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (ExistCategory == null) throw new Exception("Not found category");

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (product != null) throw new Exception("There is product in Category");

            _dbContext.Categories.Remove(ExistCategory);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CategoryInfoDto>> getAllCategoriesAsync()
        {
            var listCategories = await _dbContext.Categories.Where(x => x.Active == true).ToListAsync();

            var listCategoriesInfo = _mapper.Map<List<Category>, List<CategoryInfoDto>>(listCategories);

            return listCategoriesInfo;

        }

        public async Task<CategoryInfoDto> getCategoryByIdAsync(int id)
        {
            var Category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            var categoryInfo = _mapper.Map<CategoryInfoDto>(Category);

            return categoryInfo;
        }

        public async Task<CategoryInfoDto> updateCategoryAsync(int id, CategoryUpdateDto updateDto)
        {
            var ExistCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (ExistCategory == null) throw new Exception("No Category found");

            ExistCategory.categoryName = updateDto.Name;
            ExistCategory.Active = updateDto.Active;
            ExistCategory.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            var categoryInfo = _mapper.Map<CategoryInfoDto>(ExistCategory);

            return categoryInfo;
        }
    }
}
