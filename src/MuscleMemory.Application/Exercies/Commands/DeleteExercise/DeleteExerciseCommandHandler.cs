using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Application.Exercies.Commands.UpdateRecord;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Authorization.Services;

namespace MuscleMemory.Application.Exercies.Commands.DeleteExercise;

public class DeleteExerciseCommandHandler(ILogger<DeleteExerciseCommandHandler> logger,
    IExerciseRepository exerciseRepository,
    IUserContext userContext,
    IExerciseAuthorizationService authorizationService) : IRequestHandler<DeleteExerciseCommand>
{
    public async Task Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is deleting the exercise");

        var exercise = await exerciseRepository.GetUserExerciseByIdAsync(request.Id);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.Id.ToString());

        if (!authorizationService.Authorize(exercise)) throw new ForbidException();

        await exerciseRepository.DeleteUserExerciseById(exercise);
    }
}
