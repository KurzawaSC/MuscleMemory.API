using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Constants;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;

namespace MuscleMemory.Application.Users.Commands.SwitchAccountToNonPremium;

public class SwitchAccountToNonPremiumCommandHandler(ILogger<SwitchAccountToNonPremiumCommandHandler> logger,
    UserManager<User> userManager) : IRequestHandler<SwitchAccountToNonPremiumCommand>
{
    public async Task Handle(SwitchAccountToNonPremiumCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Switching account with email: {request.UserEmail} to non-premium");

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        await userManager.RemoveFromRoleAsync(user, UserRoles.UserPremium);
    }
}
