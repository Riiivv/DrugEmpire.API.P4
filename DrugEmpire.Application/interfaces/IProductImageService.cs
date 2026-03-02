using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageDTOResponse>> GetAllProductImages();
        Task<ProductImageDTOResponse> GetProductImageById(int id);
        Task<ProductImageDTOResponse> CreateProductImage(ProductImageDTORequest productImageDtoRequest);
        Task<ProductImageDTOResponse> UpdateProductImage(int id, ProductImageDTORequest productImageDtoRequest);
        Task<bool> DeleteProductImage(int id);
    }
}
