using AutoMapper;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;
using Newtonsoft.Json;

namespace MoneyDiary.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.InsertAsync(category);
            return category;
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            var history = _mapper.Map<CategoryHistoryDto>(category);
            var historyList = string.IsNullOrEmpty(category.History) ? [] : JsonConvert.DeserializeObject<List<CategoryHistoryDto>>(category.History);
            historyList.Add(history);

            _mapper.Map(categoryDto, category);
            category.History = JsonConvert.SerializeObject(historyList);
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.UpdateAsync(category);

            return category;
        }
    }
}