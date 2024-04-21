using MediatR;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Application.Exercies.Commands.CreateExercise;

public class CreateExerciseCommand : IRequest
{
    public string Name { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public int? Reps { get; set; } = default!;
}
