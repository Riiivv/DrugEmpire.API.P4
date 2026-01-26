using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category updateCategory);
        Task<bool> DeleteCategoryByIdAsync(int id);
    }
}
