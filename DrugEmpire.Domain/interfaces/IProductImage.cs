using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IProductImage
    {
        Task<List<ProductImage>> GetProductImagesAsync();
        Task<ProductImage> GetProductImageByIdAsync(int id);
        Task<ProductImage> CreateProductImageAsync(ProductImage productImage);
        Task<ProductImage> UpdateProductImageAsync(int id, ProductImage productImage);
        Task<bool> DeleteProductImageAsync(int id);
    }
}
