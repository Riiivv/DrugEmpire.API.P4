using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DrugEmpire.Application.services
{
    public class ProductService : IProductService
    {
        private readonly IProduct _ProductRepository;

        public ProductService(IProduct productRepository)
        {
            _ProductRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTOResponse>> GetAllProducts()
        {
            var products = await _ProductRepository.GetProductsAsync();

            return products.Select(p => new ProductDTOResponse
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Sku = p.Sku,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null
            });
        }

        public async Task<ProductDTOResponse> GetProductById(int id)
        {
            var product = await _ProductRepository.GetProductByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            return new ProductDTOResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Sku = product.Sku,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : null
            };
        }

        public async Task<ProductDTOResponse> CreateProduct(ProductDTORequest productDtoRequest)
        {
            if (productDtoRequest == null)
                throw new ArgumentNullException(nameof(productDtoRequest));

            if (string.IsNullOrWhiteSpace(productDtoRequest.Name))
                throw new Exception("Name is required");

            if (productDtoRequest.Price <= 0)
                throw new Exception("Price must be greater than 0");

            var product = new Product
            {
                Name = productDtoRequest.Name,
                Description = productDtoRequest.Description,
                Price = productDtoRequest.Price,
                Sku = productDtoRequest.Sku,
                IsActive = productDtoRequest.IsActive,
                CategoryId = productDtoRequest.CategoryId
            };

            var created = await _ProductRepository.CreateProductAsync(product);

            return new ProductDTOResponse
            {
                ProductId = created.ProductId,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                Sku = created.Sku,
                IsActive = created.IsActive,
                CategoryId = created.CategoryId,
                CategoryName = created.Category != null ? created.Category.Name : null
            };
        }

        public async Task<ProductDTOResponse> UpdateProduct(int id, ProductDTORequest productDtoRequest)
        {
            if (productDtoRequest == null)
                throw new ArgumentNullException(nameof(productDtoRequest));

            if (string.IsNullOrWhiteSpace(productDtoRequest.Name))
                throw new Exception("Name is required");

            if (productDtoRequest.Price <= 0)
                throw new Exception("Price must be greater than 0");

            var existing = await _ProductRepository.GetProductByIdAsync(id);
            if (existing == null)
                throw new Exception("Product not found");

            existing.Name = productDtoRequest.Name;
            existing.Description = productDtoRequest.Description;
            existing.Price = productDtoRequest.Price;
            existing.Sku = productDtoRequest.Sku;
            existing.IsActive = productDtoRequest.IsActive;
            existing.CategoryId = productDtoRequest.CategoryId;

            var updated = await _ProductRepository.UpdateProductAsync(id, existing);

            return new ProductDTOResponse
            {
                ProductId = updated.ProductId,
                Name = updated.Name,
                Description = updated.Description,
                Price = updated.Price,
                Sku = updated.Sku,
                IsActive = updated.IsActive,
                CategoryId = updated.CategoryId,
                CategoryName = updated.Category != null ? updated.Category.Name : null
            };
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existing = await _ProductRepository.GetProductByIdAsync(id);
            if (existing == null)
                throw new Exception("Product not found");

            await _ProductRepository.DeleteProductAsync(id);
            return true;
        }
    }
}