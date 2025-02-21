using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        // YAZAR KONTROL

        [Fact]
        public void WhenAlredyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arragne
            var book = new Book()
            {
                Title = "WhenAlredyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1990,01,10),
                GenreId = 2,
                AuthorId = 1 
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model= new CreateBookModel() {Title = book.Title};
            
            // act & assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exists"); 
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange
            _context.Books.RemoveRange(_context.Books);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobit " + Guid.NewGuid().ToString(),
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                AuthorName = "J. R. R.",
                AuthorSurname = "Tolkien"
                
            };
            command.Model = model;

            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
            
            // assert
            var book = _context.Books.SingleOrDefault(book=> book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}