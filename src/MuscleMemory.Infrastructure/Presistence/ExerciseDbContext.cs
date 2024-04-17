using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuscleMemory.Domain.Entities;

namespace MuscleMemory.Infrastructure.Presistence;

internal class ExerciseDbContext(DbContextOptions<ExerciseDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(o => o.UsersExercices)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}
