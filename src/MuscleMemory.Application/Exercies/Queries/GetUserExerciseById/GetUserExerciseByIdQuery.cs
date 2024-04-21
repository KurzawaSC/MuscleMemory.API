using MediatR;
using MuscleMemory.Application.Exercies.Dtos;

namespace MuscleMemory.Application.Exercies.Queries.GetUserExerciseById;

public class GetUserExerciseByIdQuery(Guid id) : IRequest<ExerciseDto>
{
    public Guid Id { get; set; } = id;
}
