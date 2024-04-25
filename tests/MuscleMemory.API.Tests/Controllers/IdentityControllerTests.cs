using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MuscleMemory.Application.Users.Commands.AssignUserRole;
using MuscleMemory.Application.Users.Commands.SwitchAccountToNonPremium;
using MuscleMemory.Application.Users.Commands.UpdateUserDetails;
using System.Net;
using Xunit;

namespace MuscleMemory.API.Controllers.Tests;

public class IdentityControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly IdentityController _controller;

    public IdentityControllerTests()
    {
        _controller = new IdentityController(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdateUserDetails_ValidCommand_ReturnsNoContent()
    {
        // arrange
        var command = new UpdateUserDetailsCommand();

        _mediatorMock.Setup(m => m.Send(command, default))
            .Returns(Task.FromResult(Unit.Value));

        // act
        var response = await _controller.UpdateUserDetails(command);

        //assert
        var statusCodeResult = response as StatusCodeResult;
        statusCodeResult!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SwitchAccountToPremium_ValidCommand_ReturnsNoContent()
    {
        // arrange
        var command = new SwitchAccountToPremiumCommand();

        _mediatorMock.Setup(m => m.Send(command, default))
            .Returns(Task.FromResult(Unit.Value));

        // act
        var response = await _controller.SwitchAccountToPremium(command);

        //assert
        var statusCodeResult = response as StatusCodeResult;
        statusCodeResult!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SwitchAccountToNonPremium_ValidCommand_ReturnsNoContent()
    {
        // arrange
        var command = new SwitchAccountToNonPremiumCommand();

        _mediatorMock.Setup(m => m.Send(command, default))
            .Returns(Task.FromResult(Unit.Value));

        // act
        var response = await _controller.SwitchAccountToNonPremium(command);

        //assert
        var statusCodeResult = response as StatusCodeResult;
        statusCodeResult!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }
}