using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;

namespace MuscleMemory.Application.Exercies.Commands.CreateExercise;

public class CreateExerciseCommandHandler(ILogger<CreateExerciseCommandHandler> logger,
    IMapper mapper,
    IExerciseRepository exerciseRepository,
    IUserContext userContext) : IRequestHandler<CreateExerciseCommand, Guid>
{
    public async Task<Guid> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is creating a new exercise");

        var exercise = mapper.Map<Exercise>(request);
        exercise.OwnerId = currentUser.Id;

        var id = await exerciseRepository.Create(exercise);
        return id;
    }
}
