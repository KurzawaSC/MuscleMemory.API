using Xunit;
using MuscleMemory.Domain.Constants;
using FluentAssertions;

namespace MuscleMemory.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.UserPremium)]
    [InlineData(UserRoles.Admin)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.UserPremium, UserRoles.Admin], null, null, null, null);

        // act

        var isInRole = currentUser.IsInRole(roleName);

        // assert

        isInRole.Should().BeTrue();

    }


    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.UserPremium], null, null, null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRoles.Admin);

        // assert

        isInRole.Should().BeFalse();

    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.UserPremium, UserRoles.Admin], null, null, null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert

        isInRole.Should().BeFalse();

    }
}