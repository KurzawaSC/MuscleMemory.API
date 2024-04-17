using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllAsync();
    }
}