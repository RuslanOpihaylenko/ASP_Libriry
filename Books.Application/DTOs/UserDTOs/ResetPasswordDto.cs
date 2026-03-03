using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.UserDTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
