using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Створює нову книгу разом з авторами і жанром
        /// </summary>
        /// <param name="dto">DTO для створення книги</param>
        /// <returns>Id створеної книги</returns>
        Task<int?> CreateBookAsync(BookCreateDto dto);

        /// <summary>
        /// Повертає книгу по Id
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns>BookReadDto або null, якщо не знайдено</returns>
        Task<BookReadDto?> GetBookByIdAsync(int id);

        /// <summary>
        /// Повертає всі книги
        /// </summary>
        /// <returns>Колекція BookReadDto</returns>
        Task<ICollection<BookReadDto>> GetAllBooksAsync();
        /// <summary>
        /// Return chunk of books
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<ICollection<BookReadDto>> GetChunkAsync(int pagenum, int limit);
        /// <summary>
        /// Search books
        /// </summary>
        /// <param name="author"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        Task<ICollection<BookReadDto>> SearchBooksAsync(
             string? author, int? year, string? genre);
        /// <summary>
        /// Update book by id
        /// </summary>
        Task<BookReadDto?> UpdeteBookAsync(int id, BookUpdateDto dto);
        /// <summary>
        /// Delete book by id
        /// </summary>
        Task<bool> DeleteBookAsync(int id);
        /// <summary>
        /// Delete all books
        /// </summary>
        Task<int> DeleteAllBooksAsync();
    }
}
