using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Dtos
{
    public class MovieDTO
    {
        public MovieDTO()
        {
            Genres = new List<GenreDTO>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public string Language { get; set; }
        public List<GenreDTO> Genres { get; set; }

        public Movie ToEntity()
        {
            var movie = new Movie
            {
                Id = Id,
                Title = Title,
                Overview = Overview,
                ReleaseDate = ReleaseDate,
                Rating = Rating,
                Language = Language
            };

            movie.MovieGenres = Genres
                .Select(x => new MovieGenre { Movie = movie, GenreId = x.Id })
                .ToList();

            return movie;
        }

        public static MovieDTO FromEntity(Movie movie)
        {
            var genres = movie.MovieGenres.Select(x => x.Genre).Distinct();
            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                Language = movie.Language,
                Genres = genres.Select(GenreDTO.FromEntity).ToList()
            };
        }
    }
}
