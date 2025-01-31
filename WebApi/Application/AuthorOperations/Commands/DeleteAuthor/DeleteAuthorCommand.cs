using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

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
            var author = _DbContext.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Author is not found");

            _DbContext.Authors.Remove(author);
            _DbContext.SaveChanges();
        }
    }
}