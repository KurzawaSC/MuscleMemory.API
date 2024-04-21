using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;
using MuscleMemory.Domain.Repositories;

namespace MuscleMemory.Application.Exercies.Commands.UpdateRecord;

public class UpdateExerciseCommandHandler(ILogger<UpdateExerciseCommandHandler> logger,
    IExerciseRepository exerciseRepository,
    IUserContext userContext) : IRequestHandler<UpdateExerciseCommand>
{
    public async Task Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is updating his exercise");

        var exercise = await exerciseRepository.GetUserExerciseByIdAsync(request.Id);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.Id.ToString());

        if (request.Name != null) exercise.Name = request.Name;
        if (request.Weight != null && request.Reps != null)
        {
            exercise.Record = $"{request.Weight}x{request.Reps}";
        }

        await exerciseRepository.SaveChangesAsync();
    }
}
