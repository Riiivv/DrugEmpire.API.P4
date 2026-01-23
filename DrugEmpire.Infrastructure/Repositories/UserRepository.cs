using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var ExistingUser = await _context.Users.FindAsync(id);
            if (ExistingUser == null)
            {
                throw new Exception("User not found");
            }
            return ExistingUser;
        }
        public async Task<User> CreateNewUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserAsync(int id, User updateuser)
        {
            var existingUser = await _context.Users.FindAsync(id);
           
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            
            existingUser.Username = updateuser.Username;
            existingUser.Email = updateuser.Email;
            existingUser.FirstName = updateuser.FirstName;
            existingUser.LastName = updateuser.LastName;
            existingUser.PhoneNumber = updateuser.PhoneNumber;
         
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserByidAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

