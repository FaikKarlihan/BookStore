using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.Include(x=> x.Author).SingleOrDefault(x=> x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Book already exists");

            var author = _dbContext.Authors.SingleOrDefault(x => x.Name.ToLower() == Model.AuthorName.ToLower() && x.Surname.ToLower() == Model.AuthorSurname.ToLower());   
            if (author is null)
            {
                author = new Author
                {
                    Name = Model.AuthorName,
                    Surname = Model.AuthorSurname,
                    BirthDate = Model.BirthDate
                };
                _dbContext.Authors.Add(author);
                _dbContext.SaveChanges();
            }

            book = _mapper.Map<Book>(Model);
            book.AuthorId = author.Id;
            book.Author = null;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }


        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public string AuthorName { get; set; }
            public string AuthorSurname { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}