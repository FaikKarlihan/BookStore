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
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Fact"
            };
            command.Model = model;

            // When
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Fac"
            };
            command.Model = model;

            // When
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenEmptyInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = " "
            };
            command.Model = model;

            // When
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().Be(0);
        }
    }
}