using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using Xunit;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.userId=1;
            // When
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            
            // Then
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.userId=0;
            // When
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            
            // Then
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenIdIsBlank_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteUserCommand command = new DeleteUserCommand(null);
            // When
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            
            // Then
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}