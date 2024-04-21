using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Application.Exercies.Commands.DeleteExercise;
using MuscleMemory.Application.Exercies.Commands.UpdateRecord;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Exercies.Queries.GetAllExercises;
using MuscleMemory.Application.Exercies.Queries.GetUserExerciseById;

namespace MuscleMemory.API.Controllers;

[ApiController]
[Route("api/exercises")]
[Authorize]
public class ExerciseController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAllUserExercises([FromQuery] GetAllUserExercisesQuery query)
    {
        var exercises = await mediator.Send(query);
        return Ok(exercises);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDto>> GetUserExerciseById(Guid id)
    {
        var exercise = await mediator.Send(new GetUserExerciseByIdQuery(id));
        return Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise(CreateExerciseCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRecord(Guid id,[FromBody] UpdateExerciseCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserExerciseById(Guid id)
    {
        await mediator.Send(new DeleteExerciseCommand(id));
        return NoContent();
    }
}
