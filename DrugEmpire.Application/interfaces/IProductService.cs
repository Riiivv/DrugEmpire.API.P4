using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTOResponse>> GetAllProducts();
        Task<ProductDTOResponse> GetProductById(int id);
        Task<ProductDTOResponse> CreateProduct(ProductDTORequest productDtoRequest);
        Task<ProductDTOResponse> UpdateProduct(int id, ProductDTORequest productDtoRequest);
        Task<bool> DeleteProduct(int id);
    }
}
