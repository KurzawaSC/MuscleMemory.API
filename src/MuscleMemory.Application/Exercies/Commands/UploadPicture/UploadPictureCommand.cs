using MediatR;
using Microsoft.AspNetCore.Http;

namespace MuscleMemory.Application.Exercies.Commands.UploadPicture;

public class UploadPictureCommand(Guid id, string fileName, Stream file) : IRequest
{
    public Guid Id { get; set; } = id;
    public string FileName { get; set; } = fileName;
    public Stream File { get; set; } = file;
}
