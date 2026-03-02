using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class UserService : IUserService
    {
        private readonly IUser _UserRepository;

        public UserService(IUser userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTOResponse>> GetAllUsers()
        {
            var users = await _UserRepository.GetAllUsersAsync();

            return users.Select(u => new UserDTOResponse
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role
            });
        }

        public async Task<UserDTOResponse> GetUserById(int id)
        {
            var user = await _UserRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            return new UserDTOResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };
        }

        public async Task<UserDTOResponse> CreateUser(UserDTORequest userDtoRequest)
        {
            if (userDtoRequest == null)
                throw new ArgumentNullException(nameof(userDtoRequest));

            if (string.IsNullOrWhiteSpace(userDtoRequest.Username))
                throw new Exception("Username is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.FirstName))
                throw new Exception("FirstName is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.LastName))
                throw new Exception("LastName is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.PhoneNumber))
                throw new Exception("PhoneNumber is required");

            var entity = new User
            {
                Username = userDtoRequest.Username,
                Email = userDtoRequest.Email,
                FirstName = userDtoRequest.FirstName,
                LastName = userDtoRequest.LastName,
                PhoneNumber = userDtoRequest.PhoneNumber,
                Role = userDtoRequest.Role
            };

            await _UserRepository.AddUserAsync(entity);

            // entity.UserId vil normalt være sat efter SaveChanges i repo
            return new UserDTOResponse
            {
                UserId = entity.UserId,
                Username = entity.Username,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Role = entity.Role
            };
        }

        public async Task<UserDTOResponse> UpdateUser(int id, UserDTORequest userDtoRequest)
        {
            if (userDtoRequest == null)
                throw new ArgumentNullException(nameof(userDtoRequest));

            if (string.IsNullOrWhiteSpace(userDtoRequest.Username))
                throw new Exception("Username is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.FirstName))
                throw new Exception("FirstName is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.LastName))
                throw new Exception("LastName is required");

            if (string.IsNullOrWhiteSpace(userDtoRequest.PhoneNumber))
                throw new Exception("PhoneNumber is required");

            var existing = await _UserRepository.GetUserByIdAsync(id);
            if (existing == null)
                throw new Exception("User not found");

            existing.Username = userDtoRequest.Username;
            existing.Email = userDtoRequest.Email;
            existing.FirstName = userDtoRequest.FirstName;
            existing.LastName = userDtoRequest.LastName;
            existing.PhoneNumber = userDtoRequest.PhoneNumber;
            existing.Role = userDtoRequest.Role;

            await _UserRepository.UpdateUserAsync(existing);

            return new UserDTOResponse
            {
                UserId = existing.UserId,
                Username = existing.Username,
                Email = existing.Email,
                FirstName = existing.FirstName,
                LastName = existing.LastName,
                PhoneNumber = existing.PhoneNumber,
                Role = existing.Role
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            var existing = await _UserRepository.GetUserByIdAsync(id);
            if (existing == null)
                throw new Exception("User not found");

            await _UserRepository.DeleteUserAsync(id);
            return true;
        }
    }
}