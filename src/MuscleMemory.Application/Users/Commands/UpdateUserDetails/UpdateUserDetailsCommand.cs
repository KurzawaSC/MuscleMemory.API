using MediatR;
using MuscleMemory.Domain.Constants;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public string? UserEmail { get; set; } = default!;
    public string? UserName { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; } = default(DateOnly)!;
    public string? Nationality { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
}
