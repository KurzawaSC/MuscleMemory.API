using Xunit;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Constants;

namespace MuscleMemory.Application.Users.Commands.AssignUserRole.Tests
{
    public class SwitchAccountToPremiumCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ShouldAddPremiumUserRole()
        {
            var loggerMock = new Mock<ILogger<SwitchAccountToPremiumCommandHandler>>();
            var command = new SwitchAccountToPremiumCommand()
            {
                UserEmail = "test@test.com",
            };
            var user = new User()
            {
                Id = "1",
                Email = "test@test.com",
                Nationality = "USA",
                DateOfBirth = null,
                Weight = 98,
                Height = 178,
            };

            var userManagerMock = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            userManagerMock.Setup(m => m.FindByEmailAsync("test@test.com")).ReturnsAsync(user);
            userManagerMock.Setup(m => m.AddToRoleAsync(user, UserRoles.UserPremium))
                .ReturnsAsync(IdentityResult.Success);

            var commandHandler = new SwitchAccountToPremiumCommandHandler(loggerMock.Object,
                userManagerMock.Object);

            // act
            await commandHandler.Handle(command, CancellationToken.None);

            // assert
            userManagerMock.Verify(r => r.AddToRoleAsync(user, UserRoles.UserPremium), Times.Once);
        }
    }
}