using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class AddressService : IAddressService
    {
        private readonly IAddress _AddressRepository;
        private readonly IUser _UserRepository;

        public AddressService(IAddress AddressRepository, IUser UserRepository)
        {
            _AddressRepository = AddressRepository;
            _UserRepository = UserRepository;
        }
        public async Task<IEnumerable<AddressDTOResponse>> GetAllAddresses()
        {
            var addresses = await _AddressRepository.GetAllAddressesAsync();

            return addresses.Select(a => new AddressDTOResponse
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                Name = a.Name,
                Street = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                Country = a.Country,
                PhoneNumber = a.PhoneNumber
            });
        }

        public async Task<AddressDTOResponse> GetAddressById(int id)
        {
            var address = await _AddressRepository.GetAddressByIdAsync(id);
            if (address == null) return null;

            return new AddressDTOResponse
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                Name = address.Name,
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                PhoneNumber = address.PhoneNumber
            };
        }

        public async Task<AddressDTOResponse> CreateAddress(AddressDTORequest addressDTORequest)
        {
            if (addressDTORequest == null)
                throw new ArgumentNullException(nameof(addressDTORequest));

            // Valider input
            if (addressDTORequest.UserId <= 0)
                throw new Exception("UserId is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.Street))
                throw new Exception("Street is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.City))
                throw new Exception("City is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.PostalCode))
                throw new Exception("Postal code is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.Country))
                throw new Exception("Country is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.PhoneNumber))
                throw new Exception("Phone number is required");

            // (Anbefalet) Sikr at user findes
            // Hvis din IUser.GetUserByIdAsync kaster exception, så er try/catch valgfrit.
            var user = await _UserRepository.GetUserByIdAsync(addressDTORequest.UserId);
            if (user == null)
                throw new Exception("User not found");

            var address = new Address
            {
                // AddressId skal IKKE sættes ved create
                UserId = addressDTORequest.UserId,
                Name = addressDTORequest.Name,
                Street = addressDTORequest.Street,
                City = addressDTORequest.City,
                PostalCode = addressDTORequest.PostalCode,
                Country = addressDTORequest.Country,
                PhoneNumber = addressDTORequest.PhoneNumber
            };

            var createdAddress = await _AddressRepository.CreateAddressAsync(address);

            return new AddressDTOResponse
            {
                AddressId = createdAddress.AddressId,
                UserId = createdAddress.UserId,
                Name = createdAddress.Name,
                Street = createdAddress.Street,
                City = createdAddress.City,
                PostalCode = createdAddress.PostalCode,
                Country = createdAddress.Country,
                PhoneNumber = createdAddress.PhoneNumber
            };
        }

        public async Task<AddressDTOResponse> UpdateAddress(int id, AddressDTORequest addressDTORequest)
        {
            if (addressDTORequest == null)
                throw new ArgumentNullException(nameof(addressDTORequest));

            // Valider INPUT
            if (string.IsNullOrWhiteSpace(addressDTORequest.Street))
                throw new Exception("Street is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.City))
                throw new Exception("City is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.PostalCode))
                throw new Exception("Postal code is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.Country))
                throw new Exception("Country is required");

            if (string.IsNullOrWhiteSpace(addressDTORequest.PhoneNumber))
                throw new Exception("Phone number is required");

            var existingAddress = await _AddressRepository.GetAddressByIdAsync(id);
            if (existingAddress == null)
                throw new Exception("Address not found");

            // Opdater felter
            existingAddress.Name = addressDTORequest.Name;
            existingAddress.Street = addressDTORequest.Street;
            existingAddress.City = addressDTORequest.City;
            existingAddress.PostalCode = addressDTORequest.PostalCode;
            existingAddress.Country = addressDTORequest.Country;
            existingAddress.PhoneNumber = addressDTORequest.PhoneNumber;

            var updatedAddress = await _AddressRepository.UpdateAddressAsync(id, existingAddress);

            return new AddressDTOResponse
            {
                AddressId = updatedAddress.AddressId,
                UserId = updatedAddress.UserId,
                Name = updatedAddress.Name,
                Street = updatedAddress.Street,
                City = updatedAddress.City,
                PostalCode = updatedAddress.PostalCode,
                Country = updatedAddress.Country,
                PhoneNumber = updatedAddress.PhoneNumber
            };
        }

        public async Task<bool> DeleteAddress(int id)
        {
            var existingAddress = await _AddressRepository.GetAddressByIdAsync(id);
            if (existingAddress == null)
                throw new Exception("Address not found");

            await _AddressRepository.DeleteAddressAsync(id);
            return true;
        }
    }
}