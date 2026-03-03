using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Books.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDBContext _context;

        private readonly IHashHelper _helper;

        public UserRepository(LibraryDBContext context, IHashHelper helper)
        {
            _context = context;
            _helper = helper;

        }
        public async Task<string> AddUserAsync(UserEntity user, string password)
        {
            user.PasswordHash = _helper.Hash(password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Email;
        }
        public async Task<ICollection<UserEntity>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddPasswordResetTokenAsync(PasswordResetTokenEntity token)
        {
            await _context.PasswordResetTokens.AddAsync(token);
        }

        public async Task<PasswordResetTokenEntity?> GetPasswordResetTokenAsync(string token)
        {
            return await _context.PasswordResetTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == token);

        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
