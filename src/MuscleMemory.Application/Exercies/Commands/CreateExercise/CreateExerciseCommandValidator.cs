using FluentValidation;

namespace MuscleMemory.Application.Exercies.Commands.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull()
            .Length(1, 50)
            .WithMessage("Exercise's name must be at least 1 character");

        RuleFor(dto => dto.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("You can't lift negative weight");

        RuleFor(dto => dto.Reps)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .WithMessage("Number of reps must be greater than or equal to 1");
    }
}
