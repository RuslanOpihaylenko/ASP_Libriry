using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Books.Application.DTOs.BookDTOs
{
    public class BookCreateWithImageDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public ICollection<int>? AuthorsId { get; set; }
        public int GenreId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
