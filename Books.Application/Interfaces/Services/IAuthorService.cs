using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Create new author
        /// </summary>
        /// <param name="dto">DTO for author creating</param>
        /// <returns>Id for new author</returns>
        Task<int?> CreateAuthorAsync(AuthorCreateDto dto);
        /// <summary>
        /// Get author by id
        /// </summary>
        /// <param name="id">author Id</param>
        /// <returns>AuthorReadDto або null, якщо не знайдено</returns>
        Task<AuthorReadDto?> GetAuthorByIdAsync(int id);
        /// <summary>
        /// Return all authors
        /// </summary>
        /// <returns>Колекція AuthorReadDto</returns>
        Task<ICollection<AuthorReadDto>> GetAllAuthorsAsync();
        /// <summary>
        /// Search authors
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ICollection<AuthorReadDto>> SearchAuthorsAsync(string name);
        /// <summary>
        /// Update author by id
        /// </summary>
        Task<AuthorReadDto?> UpdeteAuthorAsync(int id, AuthorUpdateDto dto);
        /// <summary>
        /// Delete author by id
        /// </summary>
        Task<bool> DeleteAuthorAsync(int id);
        /// <summary>
        /// Delete all authors
        /// </summary>
        Task<int> DeleteAllAuthorsAsync();
    }
}
