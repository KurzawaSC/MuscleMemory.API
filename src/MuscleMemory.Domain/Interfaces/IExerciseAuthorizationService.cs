using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Infrastructure.Authorization.Services
{
    public interface IExerciseAuthorizationService
    {
        bool Authorize(Exercise exercise);
    }
}