using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommmandValdiatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "Pink",
                Surname = "Floyd"
            };
            command.Model = model;
            // When
            UpdateAuthorCommandValidator validator =  new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(" ", " ")]
        [InlineData("Pink", " ")]
        [InlineData(" ", "Floyd")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname)
        {
            // Given
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname
            };
            command.Model = model;
            // When
            UpdateAuthorCommandValidator validator =  new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}