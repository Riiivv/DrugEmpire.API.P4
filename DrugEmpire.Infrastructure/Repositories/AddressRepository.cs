using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class AddressRepository : IAddress
    {
        private readonly DatabaseContext _context;
        public AddressRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            var addresses = await _context.Addresses.ToListAsync();
            return addresses;
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            var PersonAddress = await _context.Addresses.FindAsync(id);

            if (PersonAddress == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
            return PersonAddress;
        }
        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        public async Task<Address> UpdateAddressAsync(int id, Address updateaddress)
        {
            var existingaddress = await _context.Addresses.FindAsync(id);
            
            if (existingaddress == null)
            {
                throw new KeyNotFoundException("Address not found");
            }

            existingaddress.Name = updateaddress.Name;
            existingaddress.Street = updateaddress.Street;
            existingaddress.City = updateaddress.City;
            existingaddress.PostalCode = updateaddress.PostalCode;
            existingaddress.Country = updateaddress.Country;
            existingaddress.PhoneNumber = updateaddress.PhoneNumber;

            await _context.SaveChangesAsync();

            return existingaddress;

        }
        public async Task<Address> DeleteAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
           
            if(address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }
    }
}
