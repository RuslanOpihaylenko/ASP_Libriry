using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Books.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService _userService, IJwtService _jwtService, LibraryDBContext _context) :ControllerBase
    {
        private void SetRefreshTokenCookie(RefreshTokenEntity refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null)
                return Unauthorized();
            var accessToken = _jwtService.GenerateAccessToken(dto, user.Role.ToString());
            var refreshToken = _jwtService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString());
            refreshToken.UserId = user.Id;
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            SetRefreshTokenCookie(refreshToken);
            return Ok(new { accessToken });
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var user = await _userService.GetByEmailUserAsync(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var email = await _userService.CreateUserAsync(dto);

            if (email != null)
            {
                return CreatedAtAction(nameof(GetUserByEmail), new { email }, email);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] UserLoginDto dto)
        {
            // 1. Беремо токен з cookie
            var token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            //2. Шукаємо токен у базі разом із користувачем
            var refreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
                return Unauthorized();

            var user = refreshToken.User; // Наш користувач! З БД по токену дістали

            // 3. Генеруємо новий access token
            var newAccessToken = _jwtService.GenerateAccessToken(dto, user.Role.ToString());

            // 4. Генеруємо новий refresh token
            var newRefreshToken = _jwtService.GenerateRefreshToken(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            newRefreshToken.UserId = user.Id;

            // 5. Відкликаємо старий токен
            refreshToken.IsRevoked = true;

            // 6. Додаємо новий токен до користувача
            user.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            // 7. Встановлюємо новий refresh token у cookie
            SetRefreshTokenCookie(newRefreshToken);

            return Ok(new { accessToken = newAccessToken });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["refreshToken"];
            if (token == null)
                return BadRequest();

            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null)
                return NotFound();

            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();

            Response.Cookies.Delete("refreshToken");

            return Ok("Logged out");
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _userService.RequestPasswordResetAsync(dto.Email);
            return Ok("If the email exists, reset link was sent.");
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await _userService.ResetPasswordAsync(dto.Token, dto.NewPassword);
            return Ok("Password successfully changed.");
        }
    }
}
