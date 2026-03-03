using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class RefreshTokenRepository:IRefreshTokenRepository
    {
        private readonly LibraryDBContext _context;
        public RefreshTokenRepository(LibraryDBContext context)
        {
            _context = context;
        }
        public async Task<int>? AddRefreshTokenAsync(RefreshTokenEntity refreshtoken)
        {
            _context.RefreshTokens.Add(refreshtoken);
            await _context.SaveChangesAsync();
            return refreshtoken.Id;
        }
    }
}
