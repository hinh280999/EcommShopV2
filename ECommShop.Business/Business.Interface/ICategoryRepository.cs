using EcommShop.Contracts.Dtos.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommShop.Business.Business.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryInfoDto>> getAllCategoriesAsync();
        Task<CategoryInfoDto> getCategoryByIdAsync(int id);
        Task<CategoryInfoDto> createCategoryAsync(string categoryName);
        Task<CategoryInfoDto> updateCategoryAsync(int id, CategoryUpdateDto updateDto);
        Task<bool> deleteCategoryAsync(int id);
    }
}
