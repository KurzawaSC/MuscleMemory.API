using MediatR;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Application.Exercies.Queries.GetAllExercises;

public class GetAllUserExercisesQuery : IRequest<IEnumerable<ExerciseDto>>
{
    public string? SearchPhrase { get; set; }
}
