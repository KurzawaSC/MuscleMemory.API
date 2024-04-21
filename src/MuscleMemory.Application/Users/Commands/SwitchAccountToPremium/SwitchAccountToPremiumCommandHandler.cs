using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Constants;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;

namespace MuscleMemory.Application.Users.Commands.AssignUserRole;

public class SwitchAccountToPremiumCommandHandler(ILogger<SwitchAccountToPremiumCommandHandler> logger,
    UserManager<User> userManager) : IRequestHandler<SwitchAccountToPremiumCommand>
{
    public async Task Handle(SwitchAccountToPremiumCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Switching account with email: {request.UserEmail} to premium");

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        await userManager.AddToRoleAsync(user, UserRoles.UserPremium);
        
    }
}
