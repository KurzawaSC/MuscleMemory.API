using MediatR;

namespace MuscleMemory.Application.Exercies.Commands.UpdateRecord;

public class UpdateExerciseCommand : IRequest
{
    public Guid Id { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public int? Reps { get; set; } = default!;
}
