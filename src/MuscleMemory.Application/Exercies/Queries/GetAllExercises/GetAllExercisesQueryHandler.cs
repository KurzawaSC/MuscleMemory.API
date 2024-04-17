using MediatR;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;

namespace MuscleMemory.Application.Exercies.Queries.GetAllExercises;

public class GetAllExercisesQueryHandler(ILogger<GetAllExercisesQueryHandler> logger,
    IExerciseRepository exerciseRepository) : IRequestHandler<GetAllExercisesQuery, IEnumerable<Exercise>>
{
    public async Task<IEnumerable<Exercise>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Showing all exercies");

        var exercises = await exerciseRepository.GetAllAsync();

        return exercises;
    }
}
