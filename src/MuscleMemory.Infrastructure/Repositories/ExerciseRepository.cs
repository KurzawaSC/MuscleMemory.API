using Microsoft.EntityFrameworkCore;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Infrastructure.Presistence;
using MuscleMemory.Domain.Repositories;
using Azure.Core;

namespace MuscleMemory.Infrastructure.Repositories;

internal class ExerciseRepository(ExerciseDbContext dbContext) : IExerciseRepository
{
    public async Task<IEnumerable<Exercise>> GetAllUserExerciseAsync(string userId, string? searchPhrase)
    {
        var searchPhraseToLower = searchPhrase?.ToLower();

        var exercises = await dbContext.Exercises.Where(e => e.OwnerId == userId
                            && (searchPhraseToLower == null
                                || e.Name.ToLower().Contains(searchPhraseToLower))).ToListAsync();
        return exercises;
    }
    public async Task<Exercise?> GetUserExerciseByIdAsync(Guid exerciseId)
    {
        var exercise = await dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId);
        return exercise;
    }

    public async Task<Guid> Create(Exercise exercise)
    {
        await dbContext.AddAsync(exercise);
        await dbContext.SaveChangesAsync();
        return exercise.Id;

    }

    public async Task DeleteUserExerciseById(Exercise exercise)
    {
        dbContext.Remove(exercise);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
