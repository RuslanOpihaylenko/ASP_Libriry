using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        /// <summary>
        /// Get books from BD
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BookEntity>> GetAllBooksAsync();
        Task<BookEntity> GetBookByIdAsync(int id);
        Task<int?> AddBookAsync(BookEntity book, ICollection<int>? authorsId);
        Task<BookEntity> UpdeteBookById(int id, BookEntity updateBook);
        Task<int?> DeleteBookAsync(BookEntity book);
        Task<ICollection<BookEntity>> DeleteAllBooksAsync();
        Task<ICollection<BookEntity>> GetChunk(int pagenum, int limit);
        Task<ICollection<BookEntity>> SearchBooksAsync(
            string? author, int? year, string? genre);
    }
}
