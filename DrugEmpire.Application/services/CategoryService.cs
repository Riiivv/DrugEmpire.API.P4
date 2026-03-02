using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategory _CategoryRepository;
        public CategoryService(ICategory CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<IEnumerable<CategoryDTOResponse>> GetAllCategories()
        {
            var category = await _CategoryRepository.GetAllCategoryAsync();

            return category.Select(c => new CategoryDTOResponse
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
            });
        }
        public async Task<CategoryDTOResponse> GetCategoryById(int id)
        {
            var Category = await _CategoryRepository.GetCategoryByIdAsync(id);
            if (Category == null)
                throw new Exception("Category not found");

            return new CategoryDTOResponse
            {
                CategoryId = Category.CategoryId,
                Name = Category.Name,
            };
        }
        public async Task<CategoryDTOResponse> CreateCategory(CategoryDTORequest categoryDTORequest)
        {
            if (categoryDTORequest == null)
                throw new ArgumentNullException(nameof(categoryDTORequest));

            if (categoryDTORequest.CategoryId <= 0)
                throw new ArgumentException("Categoryid is required");

            var category = new Category
            {
                CategoryId = categoryDTORequest.CategoryId,
                Name = categoryDTORequest.Name,
            };

            var created = await _CategoryRepository.CreateCategory(category);

            return new CategoryDTOResponse
            {
                CategoryId = created.CategoryId,
                Name = created.Name,
            };
        }

        public async Task<CategoryDTOResponse> UpdateCategory(int id, CategoryDTORequest categoryDTORequest)
        {
            if (categoryDTORequest == null)
                throw new ArgumentNullException(nameof(categoryDTORequest));

            if (categoryDTORequest.CategoryId <= 0)
                throw new Exception("Categoryid is required");

            var existingCategory = await _CategoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                throw new Exception("category not found");

            existingCategory.Name = categoryDTORequest.Name;

            var updatedCategory = _CategoryRepository.UpdateCategoryAsync(id, existingCategory);

            return new CategoryDTOResponse
            {
                CategoryId = categoryDTORequest.CategoryId,
                Name = categoryDTORequest.Name
            };
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var existingCategory = await _CategoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                throw new Exception("Category not found");

            await _CategoryRepository.DeleteCategoryByIdAsync(id);
            return true;
        }
    }
}
