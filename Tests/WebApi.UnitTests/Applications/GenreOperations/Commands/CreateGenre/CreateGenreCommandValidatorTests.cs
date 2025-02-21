using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError() 
        {
            //RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(4);
            CreateGenreCommand command = new CreateGenreCommand(null);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "HeyYou!"
            };

            command.Model = model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("")]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = name
            };

            command.Model = model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);            
        }
    }
}