using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty();
            RuleFor(command=> command.Model.Surname).NotEmpty();
        }
    }
}