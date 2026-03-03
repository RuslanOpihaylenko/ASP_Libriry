using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<int> AddRefreshTokenAsync(RefreshTokenEntity refreshtoken);
    }
}
