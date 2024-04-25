using Xunit;
using AutoMapper;
using Moq;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using MuscleMemory.Domain.Exeptions;
using MuscleMemory.Infrastructure.Authorization.Services;

namespace MuscleMemory.Application.Exercies.Queries.GetUserExerciseById.Tests
{
    public class GetUserExerciseByIdQueryHandlerTests
    {
        public readonly Guid exerciseId = Guid.NewGuid();

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsExerciseDto()
        {
            // arrange

            var exercise = new Exercise()
            {
                Id = exerciseId,
                Name = "Deadlift",
                Record = "150x1",
                Owner = default!,
                OwnerId = "1",
                PictureUrl = "https://sampleurl.com/test",
            };

            var exerciseDto = new ExerciseDto()
            {
                Id = exerciseId,
                Name = "Deadlift",
                Record = "150x1",
                PictureUrl = "https://sampleurl.com/test",
            };

            var loggerMock = new Mock<ILogger<GetUserExerciseByIdQueryHandler>>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetUserExerciseByIdQuery(exerciseId);

            mapperMock.Setup(m => m.Map<ExerciseDto>(It.IsAny<Exercise>()))
                .Returns(new ExerciseDto()
                {
                Id = exercise.Id,
                    Name = exercise.Name,
                    Record = exercise.Record,
                    PictureUrl = exercise.PictureUrl
                });


            var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
            ExerciseRepositoryMock
                .Setup(repo => repo.GetUserExerciseByIdAsync(exerciseId))
                .ReturnsAsync(exerciseId == exercise.Id ? exercise : null);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var exerciseAuthorizationMock = new Mock<IExerciseAuthorizationService>();
            exerciseAuthorizationMock.Setup(m => m.Authorize(exercise)).Returns(true);

            var queryHandler = new GetUserExerciseByIdQueryHandler(loggerMock.Object,
                mapperMock.Object,
                ExerciseRepositoryMock.Object,
                userContextMock.Object,
                exerciseAuthorizationMock.Object);

            // act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // assert
            result.Should().BeOfType<ExerciseDto>();
            ExerciseRepositoryMock.Verify(r => r.GetUserExerciseByIdAsync(exerciseId), Times.Once);
        }

        [Fact()]
        public async Task Handle_ForInValidCommand_ThrowsNotFoundException()
        {
            // arrange

            var exercise = new Exercise()
            {
                Id = exerciseId,
                Name = "Deadlift",
                Record = "150x1",
                Owner = default!,
                OwnerId = "1",
                PictureUrl = "https://sampleurl.com/test",
            };

            var exerciseDto = new ExerciseDto()
            {
                Id = exerciseId,
                Name = "Deadlift",
                Record = "150x1",
                PictureUrl = "https://sampleurl.com/test",
            };

            var loggerMock = new Mock<ILogger<GetUserExerciseByIdQueryHandler>>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetUserExerciseByIdQuery(new Guid("4d20e68d-8798-4b5f-a038-712a2f8f0cb1"));

            mapperMock.Setup(m => m.Map<ExerciseDto>(It.IsAny<Exercise>()))
                .Returns(new ExerciseDto()
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Record = exercise.Record,
                    PictureUrl = exercise.PictureUrl
                });

            var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
            ExerciseRepositoryMock
                .Setup(repo => repo.GetUserExerciseByIdAsync(exerciseId))
                .ReturnsAsync(exerciseId == exercise.Id ? exercise : null);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var exerciseAuthorizationMock = new Mock<IExerciseAuthorizationService>();
            exerciseAuthorizationMock.Setup(m => m.Authorize(exercise)).Returns(true);

            var queryHandler = new GetUserExerciseByIdQueryHandler(loggerMock.Object,
                mapperMock.Object,
                ExerciseRepositoryMock.Object,
                userContextMock.Object,
                exerciseAuthorizationMock.Object);

            // act
            Func<Task> act = async () => await queryHandler.Handle(query, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}