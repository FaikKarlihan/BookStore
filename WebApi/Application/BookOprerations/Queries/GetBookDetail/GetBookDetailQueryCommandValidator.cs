using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryCommandValidator :AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryCommandValidator(int ids)
        {
            RuleFor(command=> command.BookId).GreaterThan(0);
            if (ids > 0)
                RuleFor(command => command.BookId).InclusiveBetween(1, ids);
        }
    }
}