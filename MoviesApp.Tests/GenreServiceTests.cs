using Autofac;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Core.Services;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoviesApp.Tests
{
    public class GenreServiceTests
    {
        private readonly IGenreService _sut;
        private DbContextOptions<AppDbContext> _options;
        public GenreServiceTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            _sut = GetService();
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfGenres()
        {
            new SeedData(_options).Seed();

            var genres = await _sut.GetGenresAsync();

            Assert.Equal(3, genres.Count);
        }

        private List<Genre> GetSampleGenres()
        {
            return new List<Genre>
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Drama" },
                new Genre { Name = "Comedy" }
            };
        }

        private IGenreService GetService()
        {
            var builder = new ContainerBuilder();
            builder.Register((c) =>
            {
                return new EfRepository(new AppDbContext(_options));
            }).AsImplementedInterfaces();
            builder.RegisterType<GenreService>().AsImplementedInterfaces();

            var cb = builder.Build();

            return cb.Resolve<IGenreService>();
        }

        private void Seed()
        {
            var genres = GetSampleGenres();

            using (var context = new AppDbContext(_options))
            {
                genres.ForEach(x =>
                {
                    context.Add(x);
                });
                context.SaveChanges();
            }
        }

    }
}
