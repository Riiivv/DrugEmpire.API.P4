using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategory _ProductCategoryRepository;

        public ProductCategoryService(IProductCategory productCategoryRepository)
        {
            _ProductCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryDTOResponse>> GetAllProductCategories()
        {
            var items = await _ProductCategoryRepository.GetProductCategoriesAsync();

            return items.Select(pc => new ProductCategoryDTOResponse
            {
                ProductId = pc.ProductId,
                CategoryId = pc.CategoryId
            });
        }

        public async Task<ProductCategoryDTOResponse> GetProductCategoryById(int id)
        {
            var pc = await _ProductCategoryRepository.GetProductCategoryByIdAsync(id);
            if (pc == null)
                throw new Exception("ProductCategory not found");

            return new ProductCategoryDTOResponse
            {
                ProductId = pc.ProductId,
                CategoryId = pc.CategoryId
            };
        }

        public async Task<ProductCategoryDTOResponse> CreateProductCategory(ProductCategoryDTORequest productCategoryDtoRequest)
        {
            if (productCategoryDtoRequest == null)
                throw new ArgumentNullException(nameof(productCategoryDtoRequest));

            if (productCategoryDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (productCategoryDtoRequest.CategoryId <= 0)
                throw new Exception("CategoryId is required");

            var entity = new ProductCategory
            {
                ProductId = productCategoryDtoRequest.ProductId,
                CategoryId = productCategoryDtoRequest.CategoryId
            };

            var created = await _ProductCategoryRepository.CreateProductCategoryAsync(entity);

            return new ProductCategoryDTOResponse
            {
                ProductId = created.ProductId,
                CategoryId = created.CategoryId
            };
        }

        public async Task<ProductCategoryDTOResponse> UpdateProductCategory(int id, ProductCategoryDTORequest productCategoryDtoRequest)
        {
            if (productCategoryDtoRequest == null)
                throw new ArgumentNullException(nameof(productCategoryDtoRequest));

            var existing = await _ProductCategoryRepository.GetProductCategoryByIdAsync(id);
            if (existing == null)
                throw new Exception("ProductCategory not found");

            existing.ProductId = productCategoryDtoRequest.ProductId;
            existing.CategoryId = productCategoryDtoRequest.CategoryId;

            var updated = await _ProductCategoryRepository.UpdateProductCategoryAsync(id, existing);

            return new ProductCategoryDTOResponse
            {
                ProductId = updated.ProductId,
                CategoryId = updated.CategoryId
            };
        }

        public async Task<bool> DeleteProductCategory(int id)
        {
            var existing = await _ProductCategoryRepository.GetProductCategoryByIdAsync(id);
            if (existing == null)
                throw new Exception("ProductCategory not found");

            await _ProductCategoryRepository.DeleteProductCategoryAsync(id);
            return true;
        }
    }
}