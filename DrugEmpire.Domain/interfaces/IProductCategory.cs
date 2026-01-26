using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategory>> GetProductCategoriesAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);  
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);
        Task<ProductCategory> UpdateProductCategoryAsync(int id, ProductCategory productCategory);
        Task<bool> DeleteProductCategoryAsync(int id);
    }
}
