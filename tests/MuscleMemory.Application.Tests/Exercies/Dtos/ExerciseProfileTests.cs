using AutoMapper;
using FluentAssertions;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Domain.Entities;
using Xunit;

namespace MuscleMemory.Application.Exercies.Dtos.Tests;

public class ExerciseProfileTests
{
    private IMapper _mapper;

    public ExerciseProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ExerciseProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForExerciseToExerciseDto_MapsCorrectly()
    {
        // arrange
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            Name = "Deep Squat",
            Record = "100x5",
            PictureUrl = null,
        };

        // act

        var exerciseDto = _mapper.Map<ExerciseDto>(exercise);

        // assert 

        exerciseDto.Should().NotBeNull();
        exerciseDto.Id.Should().Be(exercise.Id);
        exerciseDto.Name.Should().Be(exercise.Name);
        exerciseDto.Record.Should().Be(exercise.Record);
        exerciseDto.PictureUrl.Should().Be(exercise.PictureUrl);
    }

    [Fact()]
    public void CreateMap_ForCreateExerciseCommandToExercise_MapsCorrectly()
    {
        // arrange
        var command = new CreateExerciseCommand
        {
            Name = "Romanian deadlift",
            Weight = 110,
            Reps = 8,
        };

        // act

        var exercise = _mapper.Map<Exercise>(command);

        // assert 

        exercise.Should().NotBeNull();
        exercise.Name.Should().Be(command.Name);
        exercise.Record.Should().Be($"{command.Weight}x{command.Reps}");
    }
}