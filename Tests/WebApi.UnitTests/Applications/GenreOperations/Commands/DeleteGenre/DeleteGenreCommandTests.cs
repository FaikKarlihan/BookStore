using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            DeleteGenreComand command = new DeleteGenreComand(_context);
            command.GenreId = 10;
            // When & Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book type not found");
        }

        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeDeleted()
        {
            // Given
            DeleteGenreComand command = new DeleteGenreComand(_context);
            command.GenreId = 1;            
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == command.GenreId);

            genre.Should().Be(null);
        }
    }
}