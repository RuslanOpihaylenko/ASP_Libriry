using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryDBContext _context;
        public GenreRepository(LibraryDBContext context)
        {
            _context = context;
        }
        //private async Task<ICollection<BookEntity>> GetBooksAsync(ICollection<int> booksId)
        //{
        //    var books = await _context.Books.Where(a => booksId.Contains(a.Id)).ToListAsync();
        //    if (books.Count != booksId.Count)
        //        throw new Exception("Some books not found");
        //    return books;
        //}
        public async Task<int>? AddGenreAsync(GenreEntity genre)//, ICollection<int>? booksId)
        {
            //if (booksId != null)
            //    genre.Books = await GetBooksAsync(booksId);

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task<ICollection<GenreEntity>> DeleteAllGenresAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            _context.Genres.RemoveRange(genres);
            await _context.SaveChangesAsync();
            return genres;
        }

        public async Task<int?> DeleteGenreAsync(GenreEntity genre)
        {
            if(genre == null)
            {
                return null;
            }

            _context.Genres.Remove(genre);
            return await _context.SaveChangesAsync();
        }

        public async Task<ICollection<GenreEntity>> GetAllGenreAsync()
        {
            return await _context.Genres
                .Include(b => b.Books)
                .ToListAsync();
        }

        public async Task<GenreEntity> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.Include(b => b.Books).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<GenreEntity> UpdeteGenreById(int id, GenreEntity updateGenre)
        {
            var isExist = await _context.Genres
                .Include(b => b.Books)
                .FirstOrDefaultAsync(b => b.Id == id);

            if(isExist == null)
            {
                return null;
            }

            isExist.Title = updateGenre.Title;
            isExist.Books = updateGenre.Books;

            await _context.SaveChangesAsync();

            return isExist;
        }
        public async Task<ICollection<GenreEntity>> SearchGenresAsync(string title)
        {
            return await _context.Genres
                .Where(g => g.Title.Contains(title)).ToListAsync();
        }
    }
}
