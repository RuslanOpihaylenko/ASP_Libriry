using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Services;
using Books.Application.Services;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookService _bookService, IWebHostEnvironment _env) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            int? id = await _bookService.CreateBookAsync(bookDto);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetBookById), new { id }, id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("with-image")]
        public async Task<IActionResult> AddBookWithImage([FromForm] BookCreateWithImageDto dto)
        {
            string? imageUrl = null;

            if (dto.Image != null)
            {
                var folder = Path.Combine(_env.WebRootPath, "images");

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imageUrl = "/images/" + fileName;
            }
            var bookDto = new BookCreateDto
            {
                Title = dto.Title,
                Year = dto.Year,
                AuthorsId = dto.AuthorsId,
                GenreId = dto.GenreId,
                ImageUrl = imageUrl
            };
            var id = await _bookService.CreateBookAsync(bookDto);
            return Ok(id);
        }
        [HttpGet]
        [Route("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pagenum = 1, [FromQuery] int limit = 10)
        {
            var books  = await _bookService.GetChunkAsync(pagenum, limit);
            return Ok(books);
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchBooks(
            [FromQuery] string? author, [FromQuery] int? year, [FromQuery] string? genre)
        {
            var books = await _bookService.SearchBooksAsync(author, year, genre);
            return Ok(books);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            var result = await _bookService.UpdeteBookAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteBookAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var count = await _bookService.DeleteAllBooksAsync();
            return Ok($"Deleted {count} books");
        }
    }
}
