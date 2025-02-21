using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(null,null);
            query.AuthorId = 1;
            // When
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            // Then
            var result = validator.Validate(query);
            
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(null,null);
            query.AuthorId = 0;
            // When
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            // Then
            var result = validator.Validate(query);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}