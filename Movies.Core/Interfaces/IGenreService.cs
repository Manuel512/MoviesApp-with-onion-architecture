using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetGenresAsync();
    }
}
