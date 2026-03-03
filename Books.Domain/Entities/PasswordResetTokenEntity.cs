using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities
{
    public class PasswordResetTokenEntity
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public bool IsUsed { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
