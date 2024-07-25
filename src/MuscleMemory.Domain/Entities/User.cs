using Microsoft.AspNetCore.Identity;
using MuscleMemory.Domain.Constants;

namespace MuscleMemory.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; } = default(DateOnly)!;
    public string? Nationality { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
    public List<Exercise> UsersExercices { get; set; } = new BasicExercises().exercises;
}
