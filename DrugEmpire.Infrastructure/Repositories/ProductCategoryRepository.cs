using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategory
    {
        private readonly DatabaseContext _context;
        public ProductCategoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }
        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            var existingCategory = await _context.ProductCategories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            return existingCategory;
        }
        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }
        public async Task<ProductCategory> UpdateProductCategoryAsync(int id, ProductCategory productCategory)
        {
            var existingCategory = await _context.ProductCategories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            existingCategory.CategoryId = productCategory.CategoryId;
            existingCategory.ProductId = productCategory.ProductId;
            await _context.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<bool> DeleteProductCategoryAsync(int id)
        {
            var existingCategory = await _context.ProductCategories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            _context.ProductCategories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
