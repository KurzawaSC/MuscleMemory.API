using MediatR;

namespace MuscleMemory.Application.Users.Commands.SwitchAccountToNonPremium;

public class SwitchAccountToNonPremiumCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
}
