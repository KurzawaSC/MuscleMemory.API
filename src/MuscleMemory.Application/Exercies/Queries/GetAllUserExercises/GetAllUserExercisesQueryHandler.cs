using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;

namespace MuscleMemory.Application.Exercies.Queries.GetAllExercises;

public class GetAllUserExercisesQueryHandler(ILogger<GetAllUserExercisesQueryHandler> logger,
    IMapper mapper,
    IExerciseRepository exerciseRepository,
    IUserContext userContext) : IRequestHandler<GetAllUserExercisesQuery, IEnumerable<ExerciseDto>>
{
    public async Task<IEnumerable<ExerciseDto>> Handle(GetAllUserExercisesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is getting his all exercises");

        var exercises = await exerciseRepository.GetAllUserExerciseAsync(currentUser.Id, request.SearchPhrase);

        var exercisesDtos = mapper.Map<IEnumerable<ExerciseDto>>(exercises);

        return exercisesDtos;
    }
}
