using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllUserExerciseAsync(string userId, string? searchPhrase);
        Task<Exercise?> GetUserExerciseByIdAsync(Guid exerciseId);
        Task<Guid> Create(Exercise exercise);
        Task DeleteUserExerciseById(Exercise exercise);
        Task SaveChangesAsync();
    }
}