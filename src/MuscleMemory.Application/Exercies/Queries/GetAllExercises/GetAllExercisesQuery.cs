using MediatR;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Application.Exercies.Queries.GetAllExercises;

public class GetAllExercisesQuery : IRequest<IEnumerable<Exercise>>
{
}
