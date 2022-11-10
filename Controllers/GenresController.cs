using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Genres = await _genreService.GetAll();

            return Ok(Genres);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(byte id)
        {
            var genre = await _genreService.GetByID( id );
            if (genre == null)
                return NotFound($"No genre was found with this ID : {id}");
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
          await _genreService.Add(genre);
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] GenreDto dto)
        {
            var genre = await _genreService.GetByID(id);
            if (genre == null)
                return NotFound($"No genre was found with this ID : {id}");
            genre.Name = dto.Name;
            _genreService.Update(genre);
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreService.GetByID(id);
            if (genre == null)
                return NotFound($"No genre was found with this ID : {id}");
            _genreService.Delete(genre);
            return Ok();
        }
       
    }
}
