using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core.Entities
{
    public class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
