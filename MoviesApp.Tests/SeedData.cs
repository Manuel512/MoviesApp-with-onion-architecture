using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Tests
{
    public class SeedData
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public SeedData(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public List<Movie> GetSampleMovies()
        {
            return new List<Movie>
            {
                new Movie {
                    Id = 1,
                    Title = "Avengers Endgame",
                    Rating = 5,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ReleaseDate = new DateTime(2019, 4, 22),
                    //MovieGenres = new List<MovieGenre>
                    //{
                    //    new MovieGenre { MovieId = 1, GenreId = 1 },
                    //    new MovieGenre { MovieId = 1, GenreId = 3 }
                    //}
                },
                new Movie {
                    Id = 2,
                    Title = "Men in Black: International",
                    Rating = 4,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ReleaseDate = new DateTime(2019, 6, 13),
                    //MovieGenres = new List<MovieGenre>
                    //{
                    //    new MovieGenre { MovieId = 2, GenreId = 1 },
                    //    new MovieGenre { MovieId = 2, GenreId = 3 }
                    //}
                },
                new Movie {
                    Id = 3,
                    Title = "The Martian",
                    Rating = 3,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ReleaseDate = new DateTime(2015, 10, 1),
                    //MovieGenres = new List<MovieGenre>
                    //{
                    //    new MovieGenre { MovieId = 3, GenreId = 2 },
                    //    new MovieGenre { MovieId = 3, GenreId = 3 }
                    //}
                },

            };
        }

        public List<Genre> GetSampleGenres()
        {
            return new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Comedy" }
            };
        }

        public List<MovieGenre> GetSampleMovieGenres()
        {
            return new List<MovieGenre>
            {
                new MovieGenre { MovieId = 1, GenreId = 1 },
                new MovieGenre { MovieId = 1, GenreId = 3 },
                new MovieGenre { MovieId = 2, GenreId = 1 },
                new MovieGenre { MovieId = 2, GenreId = 3 },
                new MovieGenre { MovieId = 3, GenreId = 2 },
                new MovieGenre { MovieId = 3, GenreId = 3 }
            };
        }

        public void Seed()
        {
            var genres = GetSampleGenres();
            var movies = GetSampleMovies();
            var movieGenres = GetSampleMovieGenres();

            using (var context = new AppDbContext(_options))
            {
                genres.ForEach(x => context.Add(x));
                movies.ForEach(x => context.Add(x));
                movieGenres.ForEach(x => context.Add(x));

                context.SaveChanges();
            }
        }
    }
}
