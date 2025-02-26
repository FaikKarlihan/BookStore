using System;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x=> x.userId).GreaterThan(0).NotEmpty();
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
            RuleFor(x=> x.Model.Password).MinimumLength(6).NotEmpty();
            RuleFor(x=> x.Model.Email).MinimumLength(5).NotEmpty();
        }
    }
}