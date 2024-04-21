using MediatR;

namespace MuscleMemory.Application.Users.Commands.AssignUserRole;

public class SwitchAccountToPremiumCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
}
