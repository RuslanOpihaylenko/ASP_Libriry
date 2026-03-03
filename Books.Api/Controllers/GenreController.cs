using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Services;
using Books.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController(IGenreService _genreService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto genreDto)
        {
            int? id = await _genreService.CreateGenreAsync(genreDto);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetGenreById), new { id }, id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchGenres([FromQuery] string title)
        {
            var genres = await _genreService.SearchGenresAsync(title);
            return Ok(genres);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, GenreUpdateDto dto)
        {
            var result = await _genreService.UpdeteGenreAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _genreService.DeleteGenreAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var count = await _genreService.DeleteAllGenresAsync();
            return Ok($"Deleted {count} genres");
        }
    }
}
