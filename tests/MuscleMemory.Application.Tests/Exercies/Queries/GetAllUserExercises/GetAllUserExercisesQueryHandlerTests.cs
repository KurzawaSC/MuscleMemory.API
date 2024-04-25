using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Repositories;
using Xunit;

namespace MuscleMemory.Application.Exercies.Queries.GetAllExercises.Tests;

public class GetAllUserExercisesQueryHandlerTests
{
    IEnumerable<Exercise> exercises = new List<Exercise>()
        {
            new()
            {
                Id = new Guid("f430ebc3-6e21-4e84-9b35-4e59d4e570cf"),
                Name = "Deadlift",
                Record = "150x1",
                Owner = default!,
                OwnerId = "1",
                PictureUrl = "https://sampleurl.com/test",

            },
            new()
            {
                Id = new Guid("4d20e68d-8798-4b5f-a038-712a2f8f0cb1"),
                Name = "Deep Squat",
                Record = "100x5",
                Owner = default!,
                OwnerId = "1",
                PictureUrl = "https://sampleurl2.com/test",

            },
        };

    List<ExerciseDto> exercisesDtos = new List<ExerciseDto>()
        {
            new()
            {
                Id = new Guid("f430ebc3-6e21-4e84-9b35-4e59d4e570cf"),
                Name = "Deadlift",
                Record = "150x1",
                PictureUrl = "https://sampleurl.com/test",

            },
            new()
            {
                Id = new Guid("4d20e68d-8798-4b5f-a038-712a2f8f0cb1"),
                Name = "Deep Squat",
                Record = "100x5",
                PictureUrl = "https://sampleurl2.com/test",

            },
        };

    [Fact()]
    public async Task Handle_ForValidCommand_WithNullSearchPhrase_ReturnAllUsersExercises()
    {
        // arrange

        var loggerMock = new Mock<ILogger<GetAllUserExercisesQueryHandler>>();
        var mapperMock = new Mock<IMapper>();
        var query = new GetAllUserExercisesQuery();

        mapperMock.Setup(m => m.Map<IEnumerable<ExerciseDto>>(It.IsAny<IEnumerable<Exercise>>()))
        .Returns((List<Exercise> src) =>
        {
            return src.Select(exercise =>
                new ExerciseDto
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Record = exercise.Record,
                    PictureUrl = exercise.PictureUrl
                }).ToList();
        });

        var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
        ExerciseRepositoryMock
            .Setup(repo => repo.GetAllUserExerciseAsync("1", null)).ReturnsAsync(exercises);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);


        var queryHandler = new GetAllUserExercisesQueryHandler(loggerMock.Object,
            mapperMock.Object,
            ExerciseRepositoryMock.Object,
            userContextMock.Object);

        // act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // assert
        result.Should().BeOfType<List<ExerciseDto>>();
        ExerciseRepositoryMock.Verify(r => r.GetAllUserExerciseAsync("1", null), Times.Once);
    }

    [Fact()]
    public async Task Handle_ForValidCommand_WithSearchPhrase_ReturnListOfOneUsersExercise()
    {
        // arrange

        var loggerMock = new Mock<ILogger<GetAllUserExercisesQueryHandler>>();
        var mapperMock = new Mock<IMapper>();
        var query = new GetAllUserExercisesQuery()
        {
            SearchPhrase = "deadlift",
        };

        mapperMock.Setup(m => m.Map<IEnumerable<ExerciseDto>>(It.IsAny<IEnumerable<Exercise>>()))
        .Returns((List<Exercise> src) =>
        {
            return src.Select(exercise =>
                new ExerciseDto
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Record = exercise.Record,
                    PictureUrl = exercise.PictureUrl
                }).ToList();
        });

        var ExerciseRepositoryMock = new Mock<IExerciseRepository>();
        ExerciseRepositoryMock
            .Setup(repo => repo.GetAllUserExerciseAsync("1", "deadlift"))
            .ReturnsAsync(exercises.Where(e => e.Name.ToUpper().Contains("deadlift".ToUpper())).ToList());

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);


        var queryHandler = new GetAllUserExercisesQueryHandler(loggerMock.Object,
            mapperMock.Object,
            ExerciseRepositoryMock.Object,
            userContextMock.Object);

        // act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // assert
        result.Should().BeOfType<List<ExerciseDto>>();
        result.Should().ContainSingle();
        ExerciseRepositoryMock.Verify(r => r.GetAllUserExerciseAsync("1", "deadlift"), Times.Once);
    }
}