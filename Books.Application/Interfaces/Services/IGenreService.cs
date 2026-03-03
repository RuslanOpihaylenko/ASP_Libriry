using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IGenreService
    {
        /// <summary>
        /// Create new genre
        /// </summary>
        /// <param name="dto">DTO for genre creating</param>
        /// <returns>Id for new genre</returns>
        Task<int?> CreateGenreAsync(GenreCreateDto dto);
        /// <summary>
        /// Get genre by id
        /// </summary>
        /// <param name="id">genre Id</param>
        /// <returns>GenreReadDto або null, якщо не знайдено</returns>
        Task<GenreReadDto?> GetGenreByIdAsync(int id);
        /// <summary>
        /// Return all genres
        /// </summary>
        /// <returns>Колекція GenreReadDto</returns>
        Task<ICollection<GenreReadDto>> GetAllGenresAsync();
        /// <summary>
        /// Search genres
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ICollection<GenreReadDto>> SearchGenresAsync(string title);
        /// <summary>
        /// Update genre by id
        /// </summary>
        Task<GenreReadDto?> UpdeteGenreAsync(int id, GenreUpdateDto dto);
        /// <summary>
        /// Delete genre by id
        /// </summary>
        Task<bool> DeleteGenreAsync(int id);
        /// <summary>
        /// Delete all genres
        /// </summary>
        Task<int> DeleteAllGenresAsync();
    }
}
