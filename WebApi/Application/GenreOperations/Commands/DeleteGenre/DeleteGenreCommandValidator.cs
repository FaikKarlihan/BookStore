using System.Data;
using FluentValidation;

namespace WebApi.Application.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreComand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command=> command.GenreId).GreaterThan(0);
        }
    }
}