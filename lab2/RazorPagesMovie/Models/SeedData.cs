using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>());

            context.Database.EnsureCreated();

            if (context.Movie.Any())
            {
                return; // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "Побег из Шоушенка",
                    ReleaseDate = DateTime.Parse("1994-09-23"),
                    Genre = "Драма",
                    Price = 9.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Крестный отец",
                    ReleaseDate = DateTime.Parse("1972-03-24"),
                    Genre = "Криминал",
                    Price = 12.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Начало",
                    ReleaseDate = DateTime.Parse("2010-07-16"),
                    Genre = "Фантастика",
                    Price = 14.99M,
                    Rating = "PG13"
                },
                new Movie
                {
                    Title = "Интерстеллар",
                    ReleaseDate = DateTime.Parse("2014-11-07"),
                    Genre = "Фантастика",
                    Price = 11.99M,
                    Rating = "PG13"
                }
            );

            context.SaveChanges();
        }
    }
}
