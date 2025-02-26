using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using Xunit;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTests:IClassFixture <CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateUserCommand command = new UpdateUserCommand(null, null);
            UpdateUserModel model = new UpdateUserModel()
            {
                Name = "Summer",
                Surname = "Smith",
                Email = "summerfest@gmail.com",
                Password ="555666"
            };
            command.Model = model;
            command.userId = 1;

            // When
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(0,"","","","")]
        [InlineData(1,"aaa","aaa","aaa","12345")]
        [InlineData(1,"aaa","aaa","1234","aaa")]
        [InlineData(1,"","aaa","12345","123456")]
        [InlineData(1,"aaa","","12345","123456")]
        [InlineData(1,"aaa","aaa","","12456")]
        [InlineData(0,"aaa","","12345"," ")]
        [InlineData(0," ","aaa","12345","12345")]
        [InlineData(0,"aaa","","","")]
        [InlineData(1,"","","","")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(int id, string name, string Surname, string email, string password)
        {
            // Given
            UpdateUserCommand command = new UpdateUserCommand(null, null);
            UpdateUserModel model = new UpdateUserModel()
            {
                Name = name,
                Surname = Surname,
                Email = email,
                Password = password
            };
            command.Model = model;
            command.userId = id;

            // When
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);
            // Then

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}