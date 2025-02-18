using BooksServiceApi.Requests;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        public readonly IMovieService _movie;
        public MovieController(IMovieService movie)
        {
            _movie = movie;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? name,string? genre,int? page, int? pageSize)
        {

            return Ok(

                 _movie.GetAllMovies(name,genre,page,pageSize));

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNew([FromBody] CreateMovie created)
        {

            if (_movie.All().Any(g => g.name == created.name))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("movie with that name already exists")
                });
            }
            await _movie.AddNewMovie(created);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] CreateMovie created)
        {
            if (!_movie.MovieExists(id))
            {
                return new NotFoundObjectResult(new { error = "movie doesnt exists" });
            }
            await _movie.UpdateMovieById(id, created);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            if (!_movie.MovieExists(id))
            {
                return new NotFoundObjectResult(new { error = "movie doesnt exists" });
            }
            await _movie.DeleteMovieById(id);
            return Ok();
        }
        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            return  Ok(

                _movie.GetMovieById(id));
            
        }

    }
}
