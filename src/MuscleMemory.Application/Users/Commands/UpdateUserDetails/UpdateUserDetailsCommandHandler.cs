using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;

namespace MuscleMemory.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id: {currentUser.Id} is changing his details");

        var dbUser = await userStore.FindByIdAsync(currentUser.Id, cancellationToken);

        if (dbUser == null) throw new NotFoundException(nameof(User), currentUser!.Id);

        if (request.UserEmail != null) dbUser.Email = request.UserEmail;
        if (request.UserName != null) dbUser.UserName = request.UserName;
        if (request.Nationality != null) dbUser.Nationality = request.Nationality;
        if (request.DateOfBirth != null) dbUser.DateOfBirth = request.DateOfBirth;
        if (request.Weight != null) dbUser.Weight = request.Weight;
        if (request.Height != null) dbUser.Height = request.Height;

        await userStore.UpdateAsync(dbUser, cancellationToken);

    }
}
