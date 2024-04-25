﻿using Xunit;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using FluentValidation.TestHelper;

namespace MuscleMemory.Application.Exercies.Commands.UpdateExercise.Tests;

public class UpdateExerciseCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateExerciseCommand()
        {
            Name = "Deep Squat",
            Weight = 100,
            Reps = 5,
        };

        var validator = new CreateExerciseCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new CreateExerciseCommand()
        {
            Name = "",
            Weight = -20,
            Reps = 0,
        };

        var validator = new CreateExerciseCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Weight);
        result.ShouldHaveValidationErrorFor(c => c.Reps);
    }
}