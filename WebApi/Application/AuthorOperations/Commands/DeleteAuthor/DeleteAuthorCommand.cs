using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthorCommand
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _DbContext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext contex)
        {
            _DbContext = contex;
        }
        public void Handle()
        {
            var author = _DbContext.Authors.Include(x=> x.Books).SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Author is not found");

            if (author.Books.Any())
                throw new InvalidOperationException("The author whose book is published cannot be deleted. Please delete the book first.");

            _DbContext.Authors.Remove(author);
            _DbContext.SaveChanges();
        }
    }
}