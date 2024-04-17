using Microsoft.EntityFrameworkCore;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Infrastructure.Presistence;
using MuscleMemory.Domain.Repositories;

namespace MuscleMemory.Infrastructure.Repositories;

internal class ExerciseRepository(ExerciseDbContext dbContext) : IExerciseRepository
{
    public async Task<IEnumerable<Exercise>> GetAllAsync()
    {
        var exercises = await dbContext.Exercises.ToListAsync();
        return exercises;
    }
}
