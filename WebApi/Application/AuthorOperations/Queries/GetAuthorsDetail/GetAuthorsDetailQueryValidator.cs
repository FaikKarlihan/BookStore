using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorsDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(query=> query.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}