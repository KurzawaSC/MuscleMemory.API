using FluentValidation;
using MuscleMemory.Application.Exercies.Commands.UpdateRecord;

namespace MuscleMemory.Application.Exercies.Commands.UpdateExercise;

public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(1, 50)
            .WithMessage("Exercise's name must be at least 1 character");

        RuleFor(dto => dto.Weight)
            .GreaterThanOrEqualTo(0)
            .WithMessage("You can't lift negative weight");

        RuleFor(dto => dto.Reps)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Number of reps must be greater than or equal to 1");
    }
}
