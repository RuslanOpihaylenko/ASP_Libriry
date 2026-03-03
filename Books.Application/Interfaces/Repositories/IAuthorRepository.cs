using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        /// <summary>
        /// Get authors from BD
        /// </summary>
        /// <returns></returns>
        Task<ICollection<AuthorEntity>> GetAllAuthorAsync();
        Task<AuthorEntity> GetAuthorByIdAsync(int id);
        Task<int>? AddAuthorAsync(AuthorEntity author);//, ICollection<int>? booksId);
        Task<AuthorEntity> UpdeteAuthorById(int id, AuthorEntity updateAuthor);
        Task<int?> DeleteAuthorAsync(AuthorEntity author);
        Task<ICollection<AuthorEntity>> DeleteAllAuthorsAsync();
        Task<ICollection<AuthorEntity>> SearchAuthorsAsync(string name);
    }
}
