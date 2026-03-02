using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDTOResponse>> GetAllProductCategories();
        Task<ProductCategoryDTOResponse> GetProductCategoryById(int id);
        Task<ProductCategoryDTOResponse> CreateProductCategory(ProductCategoryDTORequest productCategoryDtoRequest);
        Task<ProductCategoryDTOResponse> UpdateProductCategory(int id, ProductCategoryDTORequest productCategoryDtoRequest);
        Task<bool> DeleteProductCategory(int id);

    }
}
