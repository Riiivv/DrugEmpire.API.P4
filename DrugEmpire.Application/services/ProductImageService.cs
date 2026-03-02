using DrugEmpire.Application.DTOs;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImage _ProductImageRepository;

        public ProductImageService(IProductImage productImageRepository)
        {
            _ProductImageRepository = productImageRepository;
        }

        public async Task<IEnumerable<ProductImageDTOResponse>> GetAllProductImages()
        {
            var images = await _ProductImageRepository.GetProductImagesAsync();

            return images.Select(img => new ProductImageDTOResponse
            {
                ProductImageId = img.ProductImageId,
                ProductId = img.ProductId,
                Url = img.Url,
                SortOrder = img.SortOrder
            });
        }

        public async Task<ProductImageDTOResponse> GetProductImageById(int id)
        {
            var img = await _ProductImageRepository.GetProductImageByIdAsync(id);
            if (img == null)
                throw new Exception("Product image not found");

            return new ProductImageDTOResponse
            {
                ProductImageId = img.ProductImageId,
                ProductId = img.ProductId,
                Url = img.Url,
                SortOrder = img.SortOrder
            };
        }

        public async Task<ProductImageDTOResponse> CreateProductImage(ProductImageDTORequest productImageDtoRequest)
        {
            if (productImageDtoRequest == null)
                throw new ArgumentNullException(nameof(productImageDtoRequest));

            if (productImageDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (string.IsNullOrWhiteSpace(productImageDtoRequest.Url))
                throw new Exception("Url is required");

            if (productImageDtoRequest.SortOrder < 0)
                throw new Exception("SortOrder cannot be negative");

            var entity = new ProductImage
            {
                ProductId = productImageDtoRequest.ProductId,
                Url = productImageDtoRequest.Url,
                SortOrder = productImageDtoRequest.SortOrder
            };

            var created = await _ProductImageRepository.CreateProductImageAsync(entity);

            return new ProductImageDTOResponse
            {
                ProductImageId = created.ProductImageId,
                ProductId = created.ProductId,
                Url = created.Url,
                SortOrder = created.SortOrder
            };
        }

        public async Task<ProductImageDTOResponse> UpdateProductImage(int id, ProductImageDTORequest productImageDtoRequest)
        {
            if (productImageDtoRequest == null)
                throw new ArgumentNullException(nameof(productImageDtoRequest));

            if (productImageDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (string.IsNullOrWhiteSpace(productImageDtoRequest.Url))
                throw new Exception("Url is required");

            if (productImageDtoRequest.SortOrder < 0)
                throw new Exception("SortOrder cannot be negative");

            var existing = await _ProductImageRepository.GetProductImageByIdAsync(id);
            if (existing == null)
                throw new Exception("Product image not found");

            existing.ProductId = productImageDtoRequest.ProductId;
            existing.Url = productImageDtoRequest.Url;
            existing.SortOrder = productImageDtoRequest.SortOrder;

            var updated = await _ProductImageRepository.UpdateProductImageAsync(id, existing);

            return new ProductImageDTOResponse
            {
                ProductImageId = updated.ProductImageId,
                ProductId = updated.ProductId,
                Url = updated.Url,
                SortOrder = updated.SortOrder
            };
        }

        public async Task<bool> DeleteProductImage(int id)
        {
            var existing = await _ProductImageRepository.GetProductImageByIdAsync(id);
            if (existing == null)
                throw new Exception("Product image not found");

            await _ProductImageRepository.DeleteProductImageAsync(id);
            return true;
        }
    }
}