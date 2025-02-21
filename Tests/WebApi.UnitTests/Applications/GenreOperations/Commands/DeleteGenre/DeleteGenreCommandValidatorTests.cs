using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteGenreComand command = new DeleteGenreComand(null);
            command.GenreId =0;
            // When
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeError()
        {
            // Given
            DeleteGenreComand command = new DeleteGenreComand(null);
            command.GenreId =1;
            // When
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().Be(0);
        }
    }
}