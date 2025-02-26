using System;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidator: AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x=> x.userId).GreaterThan(0).NotEmpty();
        }
    }
}