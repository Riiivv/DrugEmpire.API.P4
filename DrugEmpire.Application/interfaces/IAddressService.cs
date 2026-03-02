using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDTOResponse>> GetAllAddresses();
        Task<AddressDTOResponse> GetAddressById(int id);
        Task<AddressDTOResponse> CreateAddress(AddressDTORequest addressDtoRequest);
        Task<AddressDTOResponse> UpdateAddress(int id, AddressDTORequest addressDtoRequest);
        Task<bool> DeleteAddress(int id);
    }
}
