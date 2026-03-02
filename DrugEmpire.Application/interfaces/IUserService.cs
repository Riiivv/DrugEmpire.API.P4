using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTOResponse>> GetAllUsers();
        Task<UserDTOResponse> GetUserById(int id);
        Task<UserDTOResponse> CreateUser(UserDTORequest userDtoRequest);
        Task<UserDTOResponse> UpdateUser(int id, UserDTORequest userDtoRequest);
        Task<bool> DeleteUser(int id);

    }
}
