using Movies.Core.Entities;
using Movies.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository _repository;
        public GenreService(IRepository repository)
        {
            _repository = repository;
        }
        public Task<List<Genre>> GetGenresAsync()
        {
            return _repository.GetAllAsync<Genre>();
        }
    }
}
