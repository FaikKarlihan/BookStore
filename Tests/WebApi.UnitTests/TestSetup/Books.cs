using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange
            (
                new Book{ Title = "The Metamorphosis", GenreId = 3, PageCount = 200, PublishDate = new DateTime(1915,02,12), AuthorId = 2},
                new Book{ Title = "The Trial", GenreId = 2, PageCount = 250, PublishDate = new DateTime(1925,05,23), AuthorId = 2},
                new Book{ Title = "Crime and Punishment", GenreId = 3, PageCount = 540, PublishDate = new DateTime(1866,12,21), AuthorId = 1},
                new Book{ Title = "The Gambler", GenreId = 3, PageCount = 150, PublishDate = new DateTime(1866,03,15), AuthorId = 1}
            );
        }
    }
}