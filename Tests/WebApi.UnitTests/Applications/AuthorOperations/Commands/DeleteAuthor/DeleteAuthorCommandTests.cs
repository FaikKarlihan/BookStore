using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthorCommand;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests (CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeDeleted()
        {
            // Given
            var author = new Author()
            {
                Id = 15,
                Name = "Hayko",
                Surname = "Cepkin",
                BirthDate = DateTime.Now.Date.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 15;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Authors.SingleOrDefault(x=> x.Id == 15);

            result.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=999;
            // When & Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author is not found");
        }
        
        [Fact]
        public void WhenAnAuthorWhoseBookAlreadyExistsIsAttemptedToBeDeleted_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;
            // When & Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("The author whose book is published cannot be deleted. Please delete the book first.");
        }
    }
}