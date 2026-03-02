using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTOResponse>> GetAllCategories();
        Task<CategoryDTOResponse> GetCategoryById(int id);
        Task<CategoryDTOResponse> CreateCategory(CategoryDTORequest categoryDtoRequest);
        Task<CategoryDTOResponse> UpdateCategory(int id, CategoryDTORequest categoryDtoRequest);
        Task<bool> DeleteCategory(int id);
    }
}
