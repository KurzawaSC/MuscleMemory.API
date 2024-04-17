using MuscleMemory.Domain.Entities;
using MuscleMemory.Infrastructure.Presistence;

namespace MuscleMemory.Infrastructure.Seeders;

internal class ExerciseSeeder(ExerciseDbContext dbContext) : IExerciseSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            //if (!dbContext.Exercises.Any())
            //{
                //var exercise = GetExercises();
                //dbContext.Exercises.AddRange(exercise);
                //await dbContext.SaveChangesAsync();
            //}
        }
    }

    private IEnumerable<Exercise> GetExercises()
    {
        List<Exercise> exercises = [
            new()
                {
                    Name = "Bench Press",
                    Record = "0x0"
                },
            new()
            {
                Name = "Deep Squat",
                    Record = "0x0"
            },
            new()
            {
                Name = "Deadlift",
                    Record = "0x0"
            },
            new()
            {
                Name = "Bent Over Row",
                    Record = "0x0"
            },
            new()
            {
                Name = "Pull up",
                    Record = "0x0"
            },
            new()
            {
                Name = "Chin up",
                    Record = "0x0"
            },
            new()
            {
                Name = "Over Head Press (OHP)",
                    Record = "0x0"
            }
            ];

        return exercises;
    }
}
