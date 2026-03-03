using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.AuthorDTOs
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        //public ICollection<int>? BooksId { get; set; }
        public AuthorReadDto()
        {
        }
        public AuthorReadDto(AuthorEntity author)
        {
            Id = author.Id;
            Name = author.Name;
            Surname = author.Surname;
         //   BooksId = author.Books == null ? [] : author.Books.Select(book => book.Id).ToList();
        }
    }
}
