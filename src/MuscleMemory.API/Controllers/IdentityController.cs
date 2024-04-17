using MediatR;
using Microsoft.AspNetCore.Mvc;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
}
