using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateUserCommand command = new CreateUserCommand(null, null);
            CreateUserModel model = new CreateUserModel(){
                Name = "Walter",
                Surname = "White",
                Email = "walterwhite@outlook.com",
                Password = "999888"
            };
            command.Model = model;
            // When & Then
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]

        [InlineData("", "", "", "")]
        [InlineData("aaa", "aaa", "aaa", " ")]
        [InlineData("aaa", "aaa", " ", "aaaa")]
        [InlineData("aaa", " ", "aaa", "aaaa")]
        [InlineData(" ", "aaa", "aaa", "aaaa")]
        [InlineData("aaa", "aaa", "aaa", "aaa")]
        [InlineData("", "", "", "aaa")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname, string email, string password)
        {
            // Given
            CreateUserCommand command = new CreateUserCommand(null, null);
            CreateUserModel model = new CreateUserModel(){
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };
            command.Model = model;
            // When & Then
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);            
        }
    }
}