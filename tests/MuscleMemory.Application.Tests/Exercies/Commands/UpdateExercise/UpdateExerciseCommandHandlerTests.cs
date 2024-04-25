using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Authorization.Services;
using Xunit;

namespace MuscleMemory.Application.Exercies.Commands.UpdateRecord.Tests;

public class UpdateExerciseCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ShouldUpdateExercise()
    {
        // arrange
        var exerciseId = Guid.NewGuid();
        var loggerMock = new Mock<ILogger<UpdateExerciseCommandHandler>>();
        var command = new UpdateExerciseCommand()
        {
            Id = exerciseId,
            Reps = 1,
            Weight = 150,
        };
        var exercise = new Exercise()
        {
            Id = exerciseId,
            Name = "Deadlift",
            Record = "140x4",
        };

        var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
        ExerciseRepositoryMock
            .Setup(repo => repo.GetUserExerciseByIdAsync(exerciseId)).Returns(Task.FromResult(exercise)!);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var exerciseAuthorizationMock = new Mock<IExerciseAuthorizationService>();
        exerciseAuthorizationMock.Setup(m => m.Authorize(exercise)).Returns(true);

        var commandHandler = new UpdateExerciseCommandHandler(loggerMock.Object,
            ExerciseRepositoryMock.Object,
            userContextMock.Object,
            exerciseAuthorizationMock.Object);

        // act
        await commandHandler.Handle(command, CancellationToken.None);

        // assert
        exercise.Record.Should().Be("150x1");
        ExerciseRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}