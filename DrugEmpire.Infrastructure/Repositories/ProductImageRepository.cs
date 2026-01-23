using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    internal class ProductImageRepository:IProductImage
    {
        private readonly DatabaseContext _context;
        public ProductImageRepository(DatabaseContext context) 
        {
            _context = context;
        }
        public async Task <List<ProductImage>> GetProductImagesAsync()
        {
            return await _context.ProductImages.ToListAsync();
        }
        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            var existingImage = await _context.ProductImages.FindAsync(id);
            if (existingImage == null)
            {
                throw new Exception("Image not found");
            }
            return existingImage;
        }
        public async Task<ProductImage> CreateProductImageAsync(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }
        public async Task<ProductImage> UpdateProductImageAsync(int id, ProductImage productImage)
        {
            var existingImage = await _context.ProductImages.FindAsync(id);
            if (existingImage == null)
            {
                throw new Exception("Image not found");
            }
            existingImage.ProductImageId = productImage.ProductImageId;
            existingImage.ProductImageId = productImage.ProductId;
            existingImage.ProductImageId = productImage.SortOrder;

            await _context.SaveChangesAsync();
            return existingImage;
        }
        public async Task<bool> DeleteProductImageAsync(int id)
        {
            var existingImage = await _context.ProductImages.FindAsync(id);
            if (existingImage == null)
            {
                throw new Exception("Image not found");
            }
            _context.ProductImages.Remove(existingImage);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
