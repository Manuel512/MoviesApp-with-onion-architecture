using Movies.Core.Entities;
using Movies.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository _repository;

        public MovieService(IRepository repository)
        {
            _repository = repository;
        }

        public Task AddMovieAsync(Movie movie)
        {
            return _repository.AddAsync(movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await CheckIfMovieExists(id);
            await _repository.DeleteAsync(movie);
        }

        public async ValueTask<Movie> GetMovieAsync(int id)
        {
            var movie = await _repository.GetByIdAsync<Movie>(id);
            if (movie != null)
            {
                movie.MovieGenres = await _repository.GetAllAsync<MovieGenre>(p => p.MovieId == movie.Id, i => i.Genre);
            }
            return movie;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var movies = await _repository.GetAllAsync<Movie>();
            var movieGenres = await _repository.GetAllAsync<MovieGenre>(x => x.Genre);
            return movies;
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var movieGenres = await _repository.GetAllAsync<MovieGenre>(x => x.MovieId == movie.Id);
            await _repository.DeleteAsync(movieGenres);

            foreach (var movieGenre in movie.MovieGenres)
            {
                await _repository.AddAsync(new MovieGenre { MovieId = movie.Id, GenreId = movieGenre.GenreId });
            }
            await _repository.UpdateAsync(movie);
        }

        private async Task<Movie> CheckIfMovieExists(int id)
        {
            var existingMovie = await _repository.GetByIdAsync<Movie>(id);
            if (existingMovie == null)
            {
                throw new AggregateException("This Movie does not exists");
            }

            return existingMovie;
        }
    }
}
