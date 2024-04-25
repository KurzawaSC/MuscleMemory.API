using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using MuscleMemory.Domain.Entities;
using Xunit;

namespace MuscleMemory.Application.Users.Commands.UpdateUserDetails.Tests
{
    public class UpdateUserDetailsCommandHandlerTests
    {
        public async Task Handle_ForValidCommand_ShouldUpdateUserDetails()
        {
            // arrange
            var loggerMock = new Mock<ILogger<UpdateUserDetailsCommandHandler>>();
            var command = new UpdateUserDetailsCommand()
            {
                UserName = "Test",
                UserEmail = "test@test.com",
                Weight = 98,
                Height = 178,
                DateOfBirth = new DateOnly(09, 04, 2000),
                Nationality = null,
            };

            var user = new User()
            {
                Id = "1",
                Email = "oldtest@test.com",
                Nationality = "USA",
                DateOfBirth = null,
                Weight = 70,
                Height = 171,
            };

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "oldtest@test.com", [], "USA", null, 70, 171);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var userStoreMock = new Mock<IUserStore<User>>();
            userStoreMock.Setup(m => m.FindByIdAsync(currentUser.Id, CancellationToken.None)).ReturnsAsync(user);


            var commandHandler = new UpdateUserDetailsCommandHandler(loggerMock.Object,
                userContextMock.Object,
                userStoreMock.Object);

            // act
            await commandHandler.Handle(command, CancellationToken.None);

            // assert

            user.Email.Should().Be("test@test.com");
            user.UserName.Should().Be("Test");
            user.Nationality.Should().Be("USA");
            user.Weight.Should().Be(98);
            user.Height.Should().Be(178);
            user.DateOfBirth.Should().Be(new DateOnly(09, 04, 2000));
        }
    }
}