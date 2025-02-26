using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using Xunit;

namespace Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenRefreshTokenIsNotEmpty_Validator_ShouldNotBeReturnError()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(null, null);
            command.refreshToken = "not null";
            // When
            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenRefreshTokenIsEmpty_Validator_ShouldBeReturnError()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(null, null);
            command.refreshToken = "";
            // When
            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}