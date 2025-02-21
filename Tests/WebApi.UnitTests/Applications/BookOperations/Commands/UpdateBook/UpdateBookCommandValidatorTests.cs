using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeRetrunError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);        
        }

        [Theory]
        [InlineData(" ", 0, 0)]
        [InlineData("asd", 0, 0)]
        [InlineData("asdf", 1, 0)]
        [InlineData(" ",0, 1)]
        [InlineData("asdf",0, 1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string title, int genreId, int bookid)
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = title,
                GenreId = genreId
            };
            command.Model = model;
            command.BookId = bookid;
            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
        
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);            
        }
    }
}