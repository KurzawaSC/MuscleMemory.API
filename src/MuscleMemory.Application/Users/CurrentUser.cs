namespace MuscleMemory.Application.Users;

public record CurrentUser(string Id,
    string Email,
    IEnumerable<string> Roles,
    string? Nationality,
    DateOnly? DateOfBirth,
    double? Weight,
    double? Height)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
