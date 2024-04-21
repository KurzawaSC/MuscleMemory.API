namespace MuscleMemory.Application.Exercies.Dtos;

public class ExerciseDto
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Record { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
}
