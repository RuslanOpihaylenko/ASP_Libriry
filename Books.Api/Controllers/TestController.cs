using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Books.Api.Controllers
{
    public class User
    {
        [Range(1,100, ErrorMessage ="Erorr id")]
        public int Id { get; set; } 
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateData([FromBody] User user)
        {
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (id > 5)
            {
                return Ok();
            }
            throw new Exception("fail");
        }
    }
}

