using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Domain.Constants;

public class BasicExercises
{
    public readonly List<Exercise> exercises = [
            new()
                {
                    Id = new Guid(),
                    Name = "Bench Press",
                    Record = "0x0"
                },
            new()
            {
                Id = new Guid(),
                Name = "Deep Squat",
                    Record = "0x0"
            },
            new()
            {
                Id = new Guid(),
                Name = "Deadlift",
                    Record = "0x0"
            },
            new()
            {
                Id = new Guid(),
                Name = "Bent Over Row",
                    Record = "0x0"
            },
            new()
            {
                Id = new Guid(),
                Name = "Pull up",
                    Record = "0x0"
            },
            new()
            {
                Id = new Guid(),
                Name = "Chin up",
                    Record = "0x0"
            },
            new()
            {
                Id = new Guid(),
                Name = "Over Head Press (OHP)",
                    Record = "0x0"
            }
            ];
}
