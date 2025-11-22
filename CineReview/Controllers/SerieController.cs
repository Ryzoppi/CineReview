using CineReview.DTOs;
using CineReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieController : ControllerBase
    {
        private readonly ISerieService _serieService;

        public SerieController(ISerieService serieService)
        {
            _serieService = serieService;
        }

        // GET: api/<SerieController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var serieReadDto = await _serieService.GetAllAsync();
            return Ok(serieReadDto);
        }

        // GET api/<SerieController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serieReadDto = await _serieService.GetByIdAsync(id);

            if (serieReadDto == null)
            {
                return NotFound(new { Message = $"Série com Id={id} não encontrada." });
            }
            return Ok(serieReadDto);
        }

        // POST api/<SerieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SerieCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var serieReadDto = await _serieService.CreateAsync(dto);

            if (serieReadDto == null)
            {
                return StatusCode(500, "Erro ao criar a série no banco de dados.");
            }

            return CreatedAtAction(nameof(GetById), new { id = serieReadDto.Id }, serieReadDto);
        }

        // PUT api/<SerieController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SerieCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var serieReadDto = await _serieService.UpdateAsync(id, dto);

            if (serieReadDto == null)
            {
                var existing = await _serieService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Série com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao salvar as alterações da série no banco de dados.");
            }

            return Ok(serieReadDto);
        }

        // DELETE api/<SerieController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _serieService.DeleteAsync(id);

            if (!success)
            {
                var existing = await _serieService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Série com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao deletar a série no banco de dados.");
            }

            return Ok(new { Message = $"Série com Id={id} removida com sucesso." });
        }
    }
}
