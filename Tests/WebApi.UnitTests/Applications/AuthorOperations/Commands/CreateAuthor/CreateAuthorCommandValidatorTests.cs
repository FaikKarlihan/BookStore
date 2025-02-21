using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel(){
                Name = "FAC",
                Surname = "FA",
                BirthDate = DateTime.Now.Date.AddYears(-1)
            };
            // When
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeRetrunError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel(){
                Name = "FAC",
                Surname = "FA",
                BirthDate = DateTime.Now.Date
            };
            // When
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(" "," ")]
        [InlineData(" ","fac")]
        [InlineData("fac","")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel(){
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.Date.AddYears(-2)
            };

            // When
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}