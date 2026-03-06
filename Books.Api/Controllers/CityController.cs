using Books.Application.Commands.CreateCity;
using Books.Application.Commands.CreateCountry;
using Books.Application.Commands.DeleteCityById;
using Books.Application.Commands.UpdateCity;
using Books.Application.DTOs.CityDTOs;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.Queries.GetAllCities;
using Books.Application.Queries.GetAllCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //https://localhost:PORT/api/country
    public class CityController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _mediator.Send(new GetAllCitiesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDto dto)
        {
            var result = await _mediator.Send(new CreateCityCommand(dto.Name));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result = await _mediator.Send(new DeleteCityByIdCommand(id));

            if (result == null || result == 0)
                return NotFound();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateDto dto)
        {
            var result = await _mediator.Send(new UpdateCityCommand(id, dto.Name));

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
