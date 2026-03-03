using Books.Application.DTOs.UserDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserCreateDto dto);
        Task<ICollection<UserReadDto>> GetAllUserAsync();
        Task<UserReadDto?> GetByEmailUserAsync(string email);
        Task<UserEntity> LoginAsync(UserLoginDto dto);
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(string token, string newPassword);
    }
}
