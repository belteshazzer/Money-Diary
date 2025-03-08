using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(CategoryDto categoryDto);
        Task<Category> UpdateCategoryAsync(Guid id, CategoryDto categoryDto);
        Task DeleteCategoryAsync(Guid id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
    }
}