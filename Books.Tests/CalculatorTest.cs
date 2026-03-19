using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Books.Infrastructure.Repositories;
using Calculator;
using Microsoft.EntityFrameworkCore;

namespace Books.Tests
{
    public class CalculatorTest
    {
        private LibraryDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<LibraryDBContext>()
                .UseInMemoryDatabase(databaseName: "Library_ASP")
                .Options;

            return new LibraryDBContext(options);
        }
        [Fact]
        public void CalculatorSum_FiveAndTwo_ReturnsSeven()
        {
            int a = 5;
            int b = 2;

            int result = 7;

            int answer = Calc.Sum(a, b);
            Assert.Equal(result, answer);
        }
        [Fact]
        public void CalculatorMult_FiveAndTwo_ReturnsTen()
        {
            int a = 5;
            int b = 2;

            int result = 9;

            int answer = Calc.Mult(a, b);
            Assert.Equal(result, answer);
        }
        [Fact]
        public async Task BookRepository_GetBookById_ReturnsBookId()
        {
            int id = 1;
            var _context = GetContext();
            var repository = new BookRepository(_context);
            BookEntity answer = await repository.GetBookByIdAsync(id);

            Assert.Equal(id, answer.Id);
        }
        [Fact]
        public async Task BookRepository_DeleteBookById_ReturnsMessage()
        {
            int id = 1;
            int result = 1;
            var _context = GetContext();
            var repository = new BookRepository(_context);
            BookEntity book = await repository.GetBookByIdAsync(id);

            int? answer = await repository.DeleteBookAsync(book);
            Assert.Equal(result, answer);
        }
        [Fact]
        public async Task AuthorRepository_GetAuthorById_ReturnsAuthorId()
        {
            int id = 1;
            var _context = GetContext();
            var repository = new AuthorRepository(_context);
            AuthorEntity answer = await repository.GetAuthorByIdAsync(id);

            Assert.Equal(id, answer.Id);
        }
        [Fact]
        public async Task AuthorRepository_DeleteAuthorById_ReturnsMessage()
        {
            int id = 1;
            int result = 1;
            var _context = GetContext();
            var repository = new AuthorRepository(_context);
            AuthorEntity author = await repository.GetAuthorByIdAsync(id);

            int? answer = await repository.DeleteAuthorAsync(author);
            Assert.Equal(result, answer);
        }
    }
}