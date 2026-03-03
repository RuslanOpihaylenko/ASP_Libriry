using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Services;
using Books.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController(IAuthorService _authorService):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorCreateDto authorDto)
        {
            int? id = await _authorService.CreateAuthorAsync(authorDto);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetAuthorById), new { id }, id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchAuthors([FromQuery]string name)
        {
            var authors = await _authorService.SearchAuthorsAsync(name);
            return Ok(authors);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, AuthorUpdateDto dto)
        {
            var result = await _authorService.UpdeteAuthorAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authorService.DeleteAuthorAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var count = await _authorService.DeleteAllAuthorsAsync();
            return Ok($"Deleted {count} authors");
        }
    }
}
