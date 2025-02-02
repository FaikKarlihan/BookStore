using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _DbContext;
        public UpdateBookViewModel Model { get; set; }
        public int BookId { get; set; }
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _DbContext = context;
        }

        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x=>x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Book not found");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.Id;
            book.Title = Model.Title != default ? Model.Title : book.Title; 
            _DbContext.SaveChanges();
        }
    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}