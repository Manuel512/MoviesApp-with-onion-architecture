using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos;
using Movies.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<MovieDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Ok(movies.Select(MovieDTO.FromEntity).ToList());
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<MovieDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            if (movie == null) return NotFound();

            return Ok(MovieDTO.FromEntity(movie));
        }

        // POST api/<MoviesController>
        [HttpPost]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostMovie([FromBody] MovieDTO movie)
        {
            try
            {
                var entity = movie.ToEntity();
                await _movieService.AddMovieAsync(entity);
                movie.Id = entity.Id;
                return CreatedAtAction(nameof(GetMovie), new { id = entity.Id }, movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Oops, Something went wrong!");
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutMovie(int id, [FromBody] MovieDTO movie)
        {
            try
            {
                await _movieService.UpdateMovieAsync(movie.ToEntity());
                return Ok();
            }
            catch (AggregateException ae)
            {
                return NotFound(ae.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Oops, Something went wrong!");
            }
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Ok();
            }
            catch (AggregateException ae)
            {
                return NotFound(ae.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Oops, Something went wrong!");
            }
        }
    }
}
