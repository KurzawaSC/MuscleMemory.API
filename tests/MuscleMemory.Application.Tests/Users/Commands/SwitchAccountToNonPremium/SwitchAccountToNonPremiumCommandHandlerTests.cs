using Xunit;
using Moq;
using MuscleMemory.Domain.Entities;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace MuscleMemory.Application.Users.Commands.SwitchAccountToNonPremium.Tests
{
    public class SwitchAccountToNonPremiumCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ShouldRemovePremiumUserRole()
        {
            var loggerMock = new Mock<ILogger<SwitchAccountToNonPremiumCommandHandler>>();
            var command = new SwitchAccountToNonPremiumCommand()
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
            userManagerMock.Setup(m => m.RemoveFromRoleAsync(user, UserRoles.UserPremium))
                .ReturnsAsync(IdentityResult.Success);

            var commandHandler = new SwitchAccountToNonPremiumCommandHandler(loggerMock.Object,
                userManagerMock.Object);

            // act
            await commandHandler.Handle(command, CancellationToken.None);

            // assert
            userManagerMock.Verify(r => r.RemoveFromRoleAsync(user, UserRoles.UserPremium), Times.Once);
        }
    }
}