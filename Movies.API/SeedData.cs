using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Entities;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Look for any TODO items.
                if (dbContext.Genres.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);
            }
        }

        private static void PopulateTestData(AppDbContext dbContext)
        {
            var genres = GetSampleGenres();
            genres.ForEach(x => dbContext.Genres.Add(x));
            dbContext.SaveChanges();
        }

        private static List<Genre> GetSampleGenres()
        {
            return new List<Genre>
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Drama" },
                new Genre { Name = "Comedy" },
                new Genre { Name = "Horror" },
                new Genre { Name = "Science Fiction" },
                new Genre { Name = "Animation" }
            };
        }
    }
}
