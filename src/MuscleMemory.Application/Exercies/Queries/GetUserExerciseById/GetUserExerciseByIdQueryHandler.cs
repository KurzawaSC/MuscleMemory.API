using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Exercies.Queries.GetAllExercises;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Authorization.Services;

namespace MuscleMemory.Application.Exercies.Queries.GetUserExerciseById;

public class GetUserExerciseByIdQueryHandler(ILogger<GetUserExerciseByIdQueryHandler> logger,
    IMapper mapper,
    IExerciseRepository exerciseRepository,
    IUserContext userContext,
    IExerciseAuthorizationService authorizationService) : IRequestHandler<GetUserExerciseByIdQuery, ExerciseDto>
{
    public async Task<ExerciseDto> Handle(GetUserExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is getting his exercise");

        var exercise = await exerciseRepository.GetUserExerciseByIdAsync(request.Id);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.Id.ToString());

        if (!authorizationService.Authorize(exercise)) throw new ForbidException();

        var exerciseDto = mapper.Map<ExerciseDto>(exercise);

        return exerciseDto;
    }
}
