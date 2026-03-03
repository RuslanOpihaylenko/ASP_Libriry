using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.GenreDTOs
{
    public class GenreCreateDto
    {
        public string Title { get; set; } = string.Empty;
        //public ICollection<int>? Books { get; set; }
    }
}
