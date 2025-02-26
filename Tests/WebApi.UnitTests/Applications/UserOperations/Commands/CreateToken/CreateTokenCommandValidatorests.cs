using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateToken;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;

namespace Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "email",
                Password = "123456"
            };
            command.Model = model;
            // When
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
        
            result.Errors.Count.Should().Be(0);
        }

        [Theory]

        [InlineData(" "," ")]
        [InlineData("aaa"," ")]
        [InlineData(" ","aaabbb")]
        [InlineData("aaa","aaa")]
        [InlineData("","")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string email, string password)
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = email,
                Password = password
            };
            command.Model = model;
            // When
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
        
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}