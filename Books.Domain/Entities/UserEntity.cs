using Books.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();
        public ICollection<PasswordResetTokenEntity> PasswordResetTokens { get; set; }
    = new List<PasswordResetTokenEntity>();
        public int CityEntityId { get; set; }
        public CityEntity? CityEntity { get; set; }
    }
}
