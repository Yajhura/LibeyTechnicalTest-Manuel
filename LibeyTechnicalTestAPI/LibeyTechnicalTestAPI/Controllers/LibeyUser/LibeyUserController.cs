using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUserController : Controller
    {
        private readonly ILibeyUserAggregate _aggregate;
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }


        [HttpGet]
        [Route("{documentNumber}")]
        public async Task<IActionResult> FindResponse(string documentNumber)
        {
            var result = await _aggregate.FindResponse(documentNumber);

            if (result.IsFailure) return BadRequest(result);

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(UserUpdateorCreateCommand command)
        {
            var result = await _aggregate.Create(command);

            if (result.IsFailure) return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] LyberUserQueryInput query)
        {
            var result = await _aggregate.FindAll(query);

            if (result.IsFailure) return BadRequest(result);

            return Ok(result);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateorCreateCommand command)
        {
            var result = await _aggregate.Update(command);

            if (result.IsFailure) return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{documentNumber}")]
        public async Task<IActionResult> Delete(string documentNumber)
        {
            var result = await _aggregate.Delete(documentNumber);

            if (result.IsFailure) return UnprocessableEntity(result);

            return Ok(result);
        }

    }
}