using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Dtos
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static GenreDTO FromEntity(Genre genre)
        {
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}
