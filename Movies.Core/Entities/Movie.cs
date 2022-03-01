using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core.Entities
{
    public class Movie
    {
        public Movie()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string  Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public string Language { get; set; }
        
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
