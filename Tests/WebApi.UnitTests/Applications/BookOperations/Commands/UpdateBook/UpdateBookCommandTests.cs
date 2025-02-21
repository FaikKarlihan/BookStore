using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange
            var TempBook = new Book()
            {
                Title = "Solaris",
                PageCount = 100,
                PublishDate = new DateTime(1990,01,10),
                GenreId = 2,
                AuthorId = 1
            };
            _context.Books.Add(TempBook);
            _context.SaveChanges();

            UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
            UpdateBookViewModel updateModel = new UpdateBookViewModel()
            {
                Title = "The Solaris",
                GenreId = 1
            };
            updateCommand.Model = updateModel;
            updateCommand.BookId = 1;

            FluentActions.Invoking(()=> updateCommand.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book=> book.Id == updateCommand.BookId);

            book.Title.Should().Be(updateModel.Title);
            book.GenreId.Should().Be(updateModel.GenreId);
        }   

        [Fact]
        public void WhenInvalidIDisGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var TempBook = new Book()
            {
                Title = "LOTR",
                PageCount = 100,
                PublishDate = new DateTime(1990,01,10),
                GenreId = 2,
                AuthorId = 1
            };
            _context.Books.Add(TempBook);
            _context.SaveChanges();

            UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
            UpdateBookViewModel updateModel = new UpdateBookViewModel()
            {
                Title = "The LOTR",
                GenreId = 1
            };
            updateCommand.Model = updateModel;
            updateCommand.BookId = 9999;

            FluentActions
                .Invoking(()=> updateCommand.Handle()) 
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book not found");         
        }

    }
}