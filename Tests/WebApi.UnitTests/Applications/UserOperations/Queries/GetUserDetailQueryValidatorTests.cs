using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.userId = 1;
            // When
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            
            // Then
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.userId = 0;
            // When
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            
            // Then
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenIdIsBlank_Validator_ShouldBeReturnError()
        {
            // Given
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            // When
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            
            // Then
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}