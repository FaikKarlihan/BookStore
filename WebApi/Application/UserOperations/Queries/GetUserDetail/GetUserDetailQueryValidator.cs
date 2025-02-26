
using System.Data;
using FluentValidation;

namespace WebApi.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator: AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(x=> x.userId).GreaterThan(0).NotEmpty();
        }
    }
}