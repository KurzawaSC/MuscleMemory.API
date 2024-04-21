namespace MuscleMemory.Infrastructure.Configuration;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string PictureContainerName { get; set; } = default!;
}
