using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuscleMemory.Application.Users.Commands.AssignUserRole;
using MuscleMemory.Application.Users.Commands.SwitchAccountToNonPremium;
using MuscleMemory.Application.Users.Commands.UpdateUserDetails;
using MuscleMemory.Domain.Constants;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.API.Controllers;

[ApiController]
[Route("api/identity")]
[Authorize]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole/premium")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> SwitchAccountToPremium(SwitchAccountToPremiumCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole/nonpremium")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> SwitchAccountToNonPremium(SwitchAccountToNonPremiumCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
