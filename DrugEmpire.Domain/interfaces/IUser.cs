using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IUser
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateNewUserAsync(User user);
        Task <User>UpdateUserAsync(int id, User updateuser);
        Task<bool> DeleteUserByidAsync(int id);
        Task<User?> GetByEmail(string email);

    }
}
