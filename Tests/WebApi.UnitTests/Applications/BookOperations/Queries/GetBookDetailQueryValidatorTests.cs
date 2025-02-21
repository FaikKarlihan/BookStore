using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdisGiven_Validator_ShouldBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId = 0;

            GetBookDetailQueryCommandValidator validator = new GetBookDetailQueryCommandValidator(5);
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId = 1;

            GetBookDetailQueryCommandValidator validator = new GetBookDetailQueryCommandValidator(5);
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}