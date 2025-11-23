using CineReview.DTOs;
using CineReview.Services;
using CineReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/<ReviewController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reviewReadDto = await _reviewService.GetAllAsync();
            return Ok(reviewReadDto);
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reviewReadDto = await _reviewService.GetByIdAsync(id);

            if (reviewReadDto == null)
            {
                return NotFound(new { Message = $"Review com Id={id} não encontrada." });
            }
            return Ok(reviewReadDto);
        }

        // POST api/<ReviewController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReviewCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var reviewReadDto = await _reviewService.CreateAsync(dto);

            if (reviewReadDto == null)
            {
                return StatusCode(500, "Erro ao criar a review no banco de dados.");
            }

            return CreatedAtAction(nameof(GetById), new { userId = reviewReadDto.UserId }, reviewReadDto);
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReviewCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var reviewReadDto = await _reviewService.UpdateAsync(id, dto);

            if (reviewReadDto == null)
            {
                var existing = await _reviewService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Review com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao salvar as alterações da review no banco de dados.");
            }

            return Ok(reviewReadDto);
        }

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _reviewService.DeleteAsync(id);

            if (!success)
            {
                var existing = await _reviewService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Review com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao deletar a review no banco de dados.");
            }

            return Ok(new { Message = $"Review com Id={id} removida com sucesso." });
        }

        // GET api/<ReviewController>/filters
        [HttpGet("filters")]
        public async Task<IActionResult> FilterReviews(int? grade, int? userId, int? mediaId, string? orderBy)
        {
            var reviewReadDto = await _reviewService.FilterReviewsAsync(grade, userId, mediaId, orderBy);
            if (reviewReadDto == null)
            {
                return NotFound(new { Message = "Review não encontrada." });
            }

            return Ok(reviewReadDto);
        }
    }
}
