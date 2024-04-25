using Microsoft.Extensions.Logging;
using Moq;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Authorization.Services;
using Xunit;

namespace MuscleMemory.Application.Exercies.Commands.DeleteExercise.Tests;

public class DeleteExerciseCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ShouldDeleteExercise()
    {
        // arrange
        var exerciseId = Guid.NewGuid();
        var loggerMock = new Mock<ILogger<DeleteExerciseCommandHandler>>();
        var command = new DeleteExerciseCommand(exerciseId);
        var exercise = new Exercise()
        {
            Id = exerciseId,
            OwnerId = "1",
        };

        var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
        ExerciseRepositoryMock
            .Setup(repo => repo.GetUserExerciseByIdAsync(exerciseId)).Returns(Task.FromResult(exercise)!);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var exerciseAuthorizationMock = new Mock<IExerciseAuthorizationService>();
        exerciseAuthorizationMock.Setup(m => m.Authorize(exercise)).Returns(true);

        var commandHandler = new DeleteExerciseCommandHandler(loggerMock.Object,
            ExerciseRepositoryMock.Object,
            userContextMock.Object,
            exerciseAuthorizationMock.Object);

        // act
        await commandHandler.Handle(command, CancellationToken.None);

        // assert
        ExerciseRepositoryMock.Verify(r => r.DeleteUserExerciseById(exercise), Times.Once);
    }
}