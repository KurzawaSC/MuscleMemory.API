using MediatR;
using Microsoft.AspNetCore.Mvc;
using MuscleMemory.Application.Exercies.Queries.GetAllExercises;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.API.Controllers;

[ApiController]
[Route("api/exercises")]
public class ExerciseController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exercise>>> GetAll([FromQuery] GetAllExercisesQuery query)
    {
        var exercises = await mediator.Send(query);
        return Ok(exercises);
    }
}
