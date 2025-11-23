using CineReview.DTOs;
using CineReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userReadDto = await _userService.GetAllAsync();
            return Ok(userReadDto);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userReadDto = await _userService.GetByIdAsync(id);

            if (userReadDto == null)
            {
                return NotFound(new { Message = $"Usuário com Id={id} não encontrada." });
            }
            return Ok(userReadDto);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var userReadDto = await _userService.CreateAsync(dto);

            if (userReadDto == null)
            {
                return StatusCode(500, "Erro ao criar o usuário no banco de dados.");
            }

            return CreatedAtAction(nameof(GetById), new { id = userReadDto.Id }, userReadDto);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserCreateDto dto)
        {
            if (dto == null)
                return BadRequest("O corpo da requisição é inválido.");

            var userReadDto = await _userService.UpdateAsync(id, dto);

            if (userReadDto == null)
            {
                var existing = await _userService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Usuário com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao salvar as alterações do usuário no banco de dados.");
            }

            return Ok(userReadDto);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);

            if (!success)
            {
                var existing = await _userService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { Message = $"Usuário com Id={id} não encontrada." });
                }
                return StatusCode(500, "Erro ao deletar o usuário no banco de dados.");
            }

            return Ok(new { Message = $"Usuário com Id={id} removida com sucesso." });
        }

        // GET api/<UserController>/5/favorites
        [HttpGet("{id}/favorites")]
        public async Task<IActionResult> GetFavorites(int id) {
            var mediaReadDto = await _userService.GetAllFavoritesAsync(id);
            return Ok(mediaReadDto);
        }

        // POST api/<UserController>/5/favorites/5
        [HttpPost("{id}/favorites/{mediaId}")]
        public async Task<IActionResult> AddFavorite(int id, int mediaId)
        {
            var mediaReadDto = await _userService.AddToFavoritesAsync(id, mediaId);
            if (mediaReadDto == null)
            {
                return NotFound(new { Message = "Usuário ou Mídia não encontrado." });
            }

            return Ok(new { Message = "Mídia adicionado a lista de favoritos com sucesso." });
        }

        // DELETE api/<UserController>/5/favorites/5
        [HttpDelete("{id}/favorites/{mediaId}")]
        public async Task<IActionResult> RemoveFavorite(int id, int mediaId)
        {
            var mediaReadDto = await _userService.RemoveFromFavoritesAsync(id, mediaId);
            if (mediaReadDto == null)
            {
                return NotFound(new { Message = "Usuário ou Mídia não encontrado." });
            }

            return Ok(new { Message = "Mídia removida a lista de favoritos com sucesso." });
        }

        // GET api/<UserController>/filters
        [HttpGet("filters")]
        public async Task<IActionResult> FilterUsers(string? name, string? email, string? orderBy)
        {
            var userReadDto = await _userService.FilterUsersAsync(name, email, orderBy);
            if (userReadDto == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }

            return Ok(userReadDto);
        }

        // GET api/<UserController>/favorites/filters
        [HttpGet("{id}/favorites/filters")]
        public async Task<IActionResult> FilterFavorites(int id, string? name, string? synopsis, string? director, int? releaseYear, string? orderBy)
        {
            var userReadDto = await _userService.FilterFavoritesAsync(id, name, synopsis, director, releaseYear, orderBy);
            if (userReadDto == null)
            {
                return NotFound(new { Message = "Mídia não encontrado." });
            }

            return Ok(userReadDto);
        }
    }
}