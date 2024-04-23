using MediatR;
using Microsoft.Extensions.Logging;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Exeptions;
using MuscleMemory.Domain.Interfaces;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Authorization.Services;

namespace MuscleMemory.Application.Exercies.Commands.UploadPicture;

public class UploadPictureCommandHandler(ILogger<UploadPictureCommandHandler> logger,
    IExerciseRepository exerciseRepository,
    IUserContext userContext,
    IBlobStorageService blobStorageService,
    IExerciseAuthorizationService authorizationService) : IRequestHandler<UploadPictureCommand>
{
    public async Task Handle(UploadPictureCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;

        logger.LogInformation($"User with Id:{currentUser.Id} is uploading a picture to his exercise");

        var exercise = await exerciseRepository.GetUserExerciseByIdAsync(request.Id);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.Id.ToString());
        if (!authorizationService.Authorize(exercise)) throw new ForbidException();

        var pictureUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        exercise.PictureUrl = pictureUrl;

        await exerciseRepository.SaveChangesAsync();
    }
}
