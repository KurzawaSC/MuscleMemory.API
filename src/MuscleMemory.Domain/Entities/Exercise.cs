namespace MuscleMemory.Domain.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Record { get; set; } = default!;

    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;

}
