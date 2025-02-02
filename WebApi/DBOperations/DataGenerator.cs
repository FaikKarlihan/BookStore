using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return ;
                }
                context.Authors.AddRange(
                    new Author{
                        Name = "Fyodor",
                        Surname = "Dostoyevski",
                        BirthDate = new DateTime(1821,11,11)
                    },
                    new Author{
                        Name = "Franz",
                        Surname = "Kafka",
                        BirthDate = new DateTime(1883,07,03)
                    }
                );
                context.SaveChanges();

                context.Genres.AddRange(
                    new Genre {
                        Name = "Personal Growth"
                    },
                    new Genre {
                        Name = "Science Fiction"
                    },
                    new Genre {
                        Name = "Novel"
                    }
                );

                context.Books.AddRange(
                    new Book{
                        Title = "The Metamorphosis",
                        GenreId = 3,
                        PageCount = 200,
                        PublishDate = new DateTime(1915,02,12),
                        AuthorId = 2
                    },
                    new Book{
                        Title = "The Trial",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(1925,05,23),
                        AuthorId = 2
                    },
                    new Book{
                        Title = "Crime and Punishment",
                        GenreId = 3,
                        PageCount = 540,
                        PublishDate = new DateTime(1866,12,21),
                        AuthorId = 1
                    },
                    new Book{
                        Title = "The Gambler",
                        GenreId = 3,
                        PageCount = 150,
                        PublishDate = new DateTime(1866,03,15),
                        AuthorId = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}