using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _MovieService;
        private readonly IGenreService _GenreService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper = null, IGenreService genreService = null)
        {
            _MovieService = movieService;
            _mapper = mapper;
            _GenreService = genreService;
        }

        private new List<string> _allowedExtentions = new List<string> { ".jpg", ".png" };
        private long _maxPosterSize = 3145728;

     
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _MovieService.GetAllAsync();
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _MovieService.GetById(id);
            var data = _mapper.Map<MoviesDetailsDto>(movie);
            if (movie == null)
                return NotFound();
            return Ok(data);
        }
        [HttpGet("GetByGenreID")]
        public async Task<IActionResult> GetByGenreID(byte genreId)
        {
            var movies = await _MovieService.GetAllAsync(genreId);
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm] MovieDto dto )
        {
            if (dto.poster == null)
                return BadRequest("poster is Required");
            if (!_allowedExtentions.Contains(Path.GetExtension(dto.poster.FileName).ToLower()))
                return BadRequest("only .jpg and .png images are allowed");
            if (dto.poster.Length> _maxPosterSize)
                return BadRequest("Max size for poster is 3 megabytes");
            var isValidGenre = await _GenreService.isValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid Genre ID");
            using var dataStream=new MemoryStream();
            await dto.poster.CopyToAsync(dataStream);
            var movie = _mapper.Map<Movie>(dto);
            movie.poster=dataStream.ToArray();
            _MovieService.Add(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _MovieService.GetById(id);
            if (movie == null)
                return NotFound($" No Movie was found with this ID : {id} ");
            var isValidGenre = await _GenreService.isValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid Genre ID");
            if (dto.poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.poster.FileName).ToLower()))
                    return BadRequest("only .jpg and .png images are allowed");
                if (dto.poster.Length > _maxPosterSize)
                    return BadRequest("Max size for poster is 3 megabytes");
                using var dataStream = new MemoryStream();
                await dto.poster.CopyToAsync(dataStream);
                movie.poster = dataStream.ToArray();
            }
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoryLine = dto.StoryLine;
            movie.GenreId = dto.GenreId;
            _MovieService.Update(movie); 
            return Ok(movie);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _MovieService.GetById(id);
            if (movie == null)
                return NotFound($"No genre was found with this ID : {id}");
            _MovieService.Delete(movie);
            return Ok();
        }
    }
}
