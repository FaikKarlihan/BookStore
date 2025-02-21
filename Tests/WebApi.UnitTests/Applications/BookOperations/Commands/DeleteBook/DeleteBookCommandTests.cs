using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIDIsGiven_Book_ShouldBeDeleted()
        {
            // arrange
            var book = new Book
            {
                Title = "Hobit",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                AuthorId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            // act 
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            // assert
            var deletedBook = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            deletedBook.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIDisGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            var book = new Book
            {
                Title = "Faust",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                AuthorId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id + 1;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book is not found");
        }
    }
}