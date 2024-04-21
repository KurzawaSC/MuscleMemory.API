using MediatR;

namespace MuscleMemory.Application.Exercies.Commands.DeleteExercise;

public class DeleteExerciseCommand(Guid id) : IRequest
{
    public Guid Id { get; set; } = id;
}
