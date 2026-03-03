using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.AuthorDTOs
{
    public class AuthorUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
       // public ICollection<int>? Books { get; set; }
    }
}
