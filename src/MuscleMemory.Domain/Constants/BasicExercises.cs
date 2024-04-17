using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Domain.Constants;

public static class BasicExercises
{
    public static readonly List<Exercise> exercises = [
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
}
