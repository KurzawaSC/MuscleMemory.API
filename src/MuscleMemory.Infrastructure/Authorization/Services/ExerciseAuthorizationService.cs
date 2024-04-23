using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Infrastructure.Authorization.Services;

public class ExerciseAuthorizationService(ILogger<ExerciseAuthorizationService> logger,
    IUserContext userContext) : IExerciseAuthorizationService
{
    public bool Authorize(Exercise exercise)
    {
        var user = userContext.GetCurrentUser()!;

        logger.LogInformation($"Authorizing user with email {user.Email} for access to exercise with Id: {exercise.Id}");

        if (user.Id == exercise.OwnerId)
        {
            logger.LogInformation("Authorization has succeeded");
            return true;
        }

        return false;
    }
}
