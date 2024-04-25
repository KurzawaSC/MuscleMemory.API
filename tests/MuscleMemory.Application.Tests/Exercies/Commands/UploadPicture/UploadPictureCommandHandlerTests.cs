using Xunit;
using Moq;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using MuscleMemory.Domain.Interfaces;
using MuscleMemory.Infrastructure.Authorization.Services;

namespace MuscleMemory.Application.Exercies.Commands.UploadPicture.Tests;

public class UploadPictureCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ShouldUploadPictureToExercise_ReturnsUrlAdress()
    {
        // arrange
        var exerciseId = Guid.NewGuid();
        using (MemoryStream stream = new MemoryStream())
        {
            var command = new UploadPictureCommand(exerciseId, "Sample.jpg", stream);

            var loggerMock = new Mock<ILogger<UploadPictureCommandHandler>>();
            var exercise = new Exercise()
            {
                Id = exerciseId,
                Name = "Deadlift",
                Record = "140x4",
                PictureUrl = null,
            };

            var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
            ExerciseRepositoryMock
                .Setup(repo => repo.GetUserExerciseByIdAsync(exerciseId)).Returns(Task.FromResult(exercise)!);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var BlobStorageServiceMock = new Mock<IBlobStorageService>();
            var url = "http://sampleurl.com/test";
            BlobStorageServiceMock.Setup(blob => blob.UploadToBlobAsync(command.File, command.FileName))
                .Returns(Task.FromResult(url));

            var exerciseAuthorizationMock = new Mock<IExerciseAuthorizationService>();
            exerciseAuthorizationMock.Setup(m => m.Authorize(exercise)).Returns(true);

            var commandHandler = new UploadPictureCommandHandler(loggerMock.Object,
                ExerciseRepositoryMock.Object,
                userContextMock.Object,
                BlobStorageServiceMock.Object,
                exerciseAuthorizationMock.Object);

            // act
            await commandHandler.Handle(command, CancellationToken.None);

            // assert
            exercise.PictureUrl.Should().Be(url);
            ExerciseRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}