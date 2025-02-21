using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeUpdated()
        {
            _context.ChangeTracker.Clear();
            var genre = new Genre()
            {
                Id = 555,
                Name = "Genre",
                IsActive = false
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            // Given
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Genres",
                IsActive = true
            };
            command.Model = model;
            command.GenreId = 555;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then

            var result = _context.Genres.SingleOrDefault(x=> x.Id == command.GenreId);

            result.Name.Should().Be("Genres");
            result.IsActive.Should().Be(true);
        }

        [Fact]
        public void WhenInvalidIDIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            _context.ChangeTracker.Clear();
            var genre = new Genre()
            {
                Id = 11111,
                Name = "Genre",
                IsActive = false
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            // Given
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Genres",
                IsActive = true
            };
            command.Model = model;
            command.GenreId = 22222;
            // When & Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book type not found");
        }

        [Fact]
        public void WhenTryingToGiveAnExistingName_InvalidOperationException_ShouldBeReturn()
        {
            var genre1 = new Genre()
            {
                Id = 10,
                Name = "Genre",
                IsActive = false
            };
            var genre2 = new Genre()
            {
                Id = 11,
                Name = "Erneg",
                IsActive = false
            };
            _context.Genres.Add(genre1);
            _context.Genres.Add(genre2);
            _context.SaveChanges();

            // When & Then
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Erneg",
                IsActive = true
            };
            command.Model = model;
            command.GenreId = 10;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("A book genre with the same name already exists");
        }
    }
}