using AutoMapper;
using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashHelper _hashHelper;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService, IHashHelper hashHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashHelper = hashHelper;
        }
        public async Task<string> CreateUserAsync(UserCreateDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);
            dto.Email = dto.Email.Trim();
            return await _userRepository.AddUserAsync(entity, dto.Password);
        }
        public async Task<ICollection<UserReadDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return _mapper.Map<ICollection<UserReadDto>>(users);
        }
        public async Task<UserReadDto?> GetByEmailUserAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;

            return _mapper.Map<UserReadDto>(user);
        }
        public async Task<UserEntity> LoginAsync(UserLoginDto dto)
        {
            dto.Email = dto.Email.Trim();
            // Чи існує користувач з таким email
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Невірний email або пароль");
            }

            // Перевірка валідності пароля
            if (!_hashHelper.IsValidPassword(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Неверный логин или пароль");
            }
            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Користувача заблоковано");

            }
            return user;
        }
        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email.Trim());

            if (user == null)
                return;

            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var resetToken = new PasswordResetTokenEntity
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddHours(1),
                IsUsed = false,
                UserId = user.Id
            };

            await _userRepository.AddPasswordResetTokenAsync(resetToken);
            await _userRepository.SaveChangesAsync();

            Console.WriteLine($"RESET TOKEN: {resetToken.Token}");
        }
        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            var resetToken = await _userRepository.GetPasswordResetTokenAsync(token);

            if (resetToken == null ||
                resetToken.IsUsed ||
                resetToken.Expires < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token");
            }

            var user = resetToken.User;

            user.PasswordHash = _hashHelper.Hash(newPassword);

            resetToken.IsUsed = true;

            foreach (var rt in user.RefreshTokens)
            {
                rt.IsRevoked = true;
            }

            await _userRepository.SaveChangesAsync();
        }
    }
}
