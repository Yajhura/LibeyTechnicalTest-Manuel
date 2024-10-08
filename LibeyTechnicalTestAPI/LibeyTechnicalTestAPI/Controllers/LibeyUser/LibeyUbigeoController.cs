using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUbigeoController : Controller
    {
        private readonly ILibeyUbigeoAggregate _aggregate;

        public LibeyUbigeoController(ILibeyUbigeoAggregate aggregate)
        {
            _aggregate = aggregate;
        }


        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] LibeyUbigeoQueryInput query)
        {
            var result = await _aggregate.FindAll(query);

            if (result.IsFailure) return BadRequest(result);

            return Ok(result);

        }
    }
}
