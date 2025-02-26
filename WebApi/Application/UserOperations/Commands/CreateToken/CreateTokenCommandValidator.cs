using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator: AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(x=> x.Model.Email).NotEmpty();
            RuleFor(x=> x.Model.Password).MinimumLength(6).NotEmpty();
        }
    }
}