using Autofac;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Core.Services;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoviesApp.Tests
{
    public class MovieServiceTests
    {
        [Fact]
        public async void GetMoviesAsync_ShouldReturnNonEmptyListOfMovies()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);

            var movies = await _sut.GetMoviesAsync();

            Assert.Equal(3, movies.Count);
        }
        
        [Fact]
        public async void GetMoviesAsync_ShouldReturnListOfMoviesWithGenders()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);

            var movies = await _sut.GetMoviesAsync();

            foreach (var movie in movies)
            {
                Assert.True(movie.MovieGenres.Count > 0);
                foreach (var movieGenre in movie.MovieGenres)
                {
                    Assert.NotNull(movieGenre.Genre);
                }
            }
        }

        [Fact]
        public async void GetMovieAsync_ShouldReturnMovie()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);

            var movie = await _sut.GetMovieAsync(1);

            Assert.NotNull(movie);
            Assert.Equal(1, movie.Id);
        }

        [Fact]
        public async void GetMovieAsync_ShouldReturnMovieWithGender()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);

            var movie = await _sut.GetMovieAsync(1);

            Assert.True(movie.MovieGenres.Count > 0);
            foreach (var movieGender in movie.MovieGenres)
            {
                Assert.NotNull(movieGender.Genre);
            }
        }

        [Fact]
        public async void AddMovieAsync_WithValidValues_ShouldSaveSuccessfully()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var movie = seedData.GetSampleMovies()[0];
            movie.Title = "Entangled";
            movie.Id = 0;
            var _sut = GetService(options);

            await _sut.AddMovieAsync(movie);


            Assert.True(movie.Id > 0);
        }

        [Fact]
        public async void UpdateMovieAsync_WithValidValues_ShouldUpdateSuccessfully()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);
            var movie = await _sut.GetMovieAsync(1);
            movie.Title = "Entangled";

            await _sut.UpdateMovieAsync(movie);
            var updatedMovie = await _sut.GetMovieAsync(1);

            Assert.Equal(movie.Title, updatedMovie.Title);
        }

        [Fact]
        public async void DeleteMovieAsync_WithValidId_ShouldDeleteSuccessfully()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}-MovieApp").Options;
            var seedData = new SeedData(options);
            seedData.Seed();
            var _sut = GetService(options);
            var movie = await _sut.GetMovieAsync(1);

            await _sut.DeleteMovieAsync(movie.Id);
            var deletedMovie = await _sut.GetMovieAsync(1);

            Assert.Null(deletedMovie);
        }

        private IMovieService GetService(DbContextOptions<AppDbContext> options)
        {
            var builder = new ContainerBuilder();
            builder.Register((c) =>
            {
                return new EfRepository(new AppDbContext(options));
            }).AsImplementedInterfaces();
            builder.RegisterType<MovieService>().AsImplementedInterfaces();

            var cb = builder.Build();

            return cb.Resolve<IMovieService>();
        }
    }
}
