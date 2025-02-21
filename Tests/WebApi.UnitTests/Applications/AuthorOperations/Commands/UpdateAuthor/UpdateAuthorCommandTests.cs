using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommmandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommmandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeUpdated()
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

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "Okyah",
                Surname = "Nikcep"
            };
            command.Model = model;
            command.AuthorId = 15;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Authors.SingleOrDefault(x=> x.Id == 15);

            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Author_ShouldBeUpdated()
        {
            // Given
            var author = new Author()
            {
                Id = 666,
                Name = "Hayko",
                Surname = "Cepkin",
                BirthDate = DateTime.Now.Date.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "Okyah",
                Surname = "Nikcep"
            };
            command.Model = model;
            command.AuthorId = 999;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found");
        }
    }
}