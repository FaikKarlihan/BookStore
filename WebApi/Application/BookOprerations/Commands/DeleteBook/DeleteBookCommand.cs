using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _DbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _DbContext = context;
        }

        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x=>x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Book is not found");

            _DbContext.Books.Remove(book);
            _DbContext.SaveChanges();
        }
    }
}