using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IAddress
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<Address> GetAddressByIdAsync(int id);
        Task<Address> CreateAddressAsync(Address address);
        Task<Address> UpdateAddressAsync(int id, Address address);
        Task<Address>DeleteAddressAsync(int id);    
    }
}
