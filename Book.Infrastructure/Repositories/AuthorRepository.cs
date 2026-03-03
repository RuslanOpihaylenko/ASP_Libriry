using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDBContext _context;
        public AuthorRepository(LibraryDBContext context)
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
        public async Task<int>? AddAuthorAsync(AuthorEntity author) //ICollection<int>? booksId)
        {
            //if (booksId != null)
            //    author.Books = await GetBooksAsync(booksId);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }

        public async Task<ICollection<AuthorEntity>> DeleteAllAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            _context.Authors.RemoveRange(authors);
            await _context.SaveChangesAsync();
            return authors;
        }

        public async Task<int?> DeleteAuthorAsync(AuthorEntity author)
        {
            if(author == null)
            {
                return null;
            }
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync();
        }

        public async Task<ICollection<AuthorEntity>> GetAllAuthorAsync()
        {
            return await _context.Authors
                .Include(b => b.Books)
                .ToListAsync();
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.Include(b => b.Books).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<AuthorEntity> UpdeteAuthorById(int id, AuthorEntity updateAuthor)
        {
            var isExist = await _context.Authors
                .Include(b => b.Books)
                .FirstOrDefaultAsync(b => b.Id == id);

            if(isExist == null)
            {
                return null;
            }

            isExist.Name = updateAuthor.Name;
            isExist.Surname = updateAuthor.Surname;
            isExist.Books = updateAuthor.Books;

            await _context.SaveChangesAsync();

            return isExist;
        }
        public async Task<ICollection<AuthorEntity>> SearchAuthorsAsync(string name)
        {
            return await _context.Authors
                .Where(a=>a.Name.Contains(name) || a.Surname.Contains(name))
                .ToListAsync();
        }
    }
}
