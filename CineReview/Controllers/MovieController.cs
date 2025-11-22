using CineReview.DTOs;
using CineReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movieReadDto = await _movieService.GetAllAsync();
            return Ok(movieReadDto);
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movieReadDto = await _movieService.GetByIdAsync(id);

            if (movieReadDto == null)
            {
                return NotFound(new { Message = $"Filme com Id={id} não encontrada." });
            }
            return Ok(movieReadDto);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var movieReadDto = await _movieService.CreateAsync(dto);

            if (movieReadDto == null)
            {
                return StatusCode(500, "Erro ao criar o filme no banco de dados.");
            }

            return CreatedAtAction(nameof(GetById), new { id = movieReadDto.Id }, movieReadDto);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var movieReadDto = await _movieService.UpdateAsync(id, dto);

            if (movieReadDto == null)
            {
                var existing = await _movieService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Filme com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao salvar as alterações do filme no banco de dados.");
            }

            return Ok(movieReadDto);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _movieService.DeleteAsync(id);

            if (!success)
            {
                var existing = await _movieService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Filme com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao deletar o filme no banco de dados.");
            }

            return Ok(new { Message = $"Filme com Id={id} removida com sucesso." });
        }
    }
}
