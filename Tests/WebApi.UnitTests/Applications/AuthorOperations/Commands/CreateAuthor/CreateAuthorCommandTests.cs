using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture <CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // Given
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Rabindranath",
                Surname = "Tagore",
                BirthDate = new DateTime (07/05/1861)
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var author = _context.Authors.SingleOrDefault(x=> x.Name == model.Name);

            author.Should().NotBeNull();
            author.Surname.Should().Be(model.Surname);
        }

        [Fact]
        public void WhenAlreadyExistsAuthorAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            var tempAuthor = new Author()
            {
                Name = "Rabindranathh",
                Surname = "Tagoree",
                BirthDate = new DateTime (07/05/1861)
            };
            _context.Authors.Add(tempAuthor);
            _context.SaveChanges();
            // Given
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Rabindranathh",
                Surname = "Tagoree",
                BirthDate = new DateTime (07/05/1861)
            };
            command.Model = model;
            // When & Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author already exists");
        }
    }
}