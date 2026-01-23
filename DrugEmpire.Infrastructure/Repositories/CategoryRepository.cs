using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly DatabaseContext _context;
        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                throw new Exception("Category can't be null");
            }
            return existingCategory;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category>UpdateCategoryAsync(int id, Category updateCategory)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }

            existingCategory.Name = updateCategory.Name;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            _context.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
