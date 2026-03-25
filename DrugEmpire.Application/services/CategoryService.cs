using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;

namespace DrugEmpire.Application.services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategory _categoryRepository;

        public CategoryService(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTOResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();

            return categories.Select(c => new CategoryDTOResponse
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            });
        }

        public async Task<CategoryDTOResponse> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            return new CategoryDTOResponse
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<CategoryDTOResponse> CreateCategory(CategoryDTORequest categoryDTORequest)
        {
            if (categoryDTORequest == null)
                throw new ArgumentNullException(nameof(categoryDTORequest));

            if (string.IsNullOrWhiteSpace(categoryDTORequest.Name))
                throw new ArgumentException("Category name is required");

            var category = new Category
            {
                Name = categoryDTORequest.Name
            };

            var created = await _categoryRepository.CreateCategory(category);

            return new CategoryDTOResponse
            {
                CategoryId = created.CategoryId,
                Name = created.Name
            };
        }

        public async Task<CategoryDTOResponse> UpdateCategory(int id, CategoryDTORequest categoryDTORequest)
        {
            if (categoryDTORequest == null)
                throw new ArgumentNullException(nameof(categoryDTORequest));

            if (string.IsNullOrWhiteSpace(categoryDTORequest.Name))
                throw new ArgumentException("Category name is required");

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
                throw new Exception("Category not found");

            existingCategory.Name = categoryDTORequest.Name;

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(id, existingCategory);

            return new CategoryDTOResponse
            {
                CategoryId = updatedCategory.CategoryId,
                Name = updatedCategory.Name
            };
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
                throw new Exception("Category not found");

            await _categoryRepository.DeleteCategoryByIdAsync(id);
            return true;
        }
    }
}