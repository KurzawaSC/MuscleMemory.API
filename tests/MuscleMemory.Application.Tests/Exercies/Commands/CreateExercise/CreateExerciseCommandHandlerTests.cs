using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using Xunit;

namespace MuscleMemory.Application.Exercies.Commands.CreateExercise.Tests;

public class CreateExerciseCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ShouldCreateExercise()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateExerciseCommandHandler>>();
        var mapperMock = new Mock<IMapper>();
        var command = new CreateExerciseCommand();
        var exercise = new Exercise();

        mapperMock.Setup(m => m.Map<Exercise>(command)).Returns(exercise);

        var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
        ExerciseRepositoryMock
            .Setup(repo => repo.Create(It.IsAny<Exercise>()));

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);


        var commandHandler = new CreateExerciseCommandHandler(loggerMock.Object,
            mapperMock.Object,
            ExerciseRepositoryMock.Object,
            userContextMock.Object);

        // act
        await commandHandler.Handle(command, CancellationToken.None);

        // assert
        exercise.OwnerId.Should().Be("1");
        ExerciseRepositoryMock.Verify(r => r.Create(exercise), Times.Once);
    }
}