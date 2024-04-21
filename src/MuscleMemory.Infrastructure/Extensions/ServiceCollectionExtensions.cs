using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MuscleMemory.Infrastructure.Presistence;
using MuscleMemory.Infrastructure.Repositories;
using MuscleMemory.Infrastructure.Seeders;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace MuscleMemory.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ExerciseDB");
        services.AddDbContext<ExerciseDbContext>(options => options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ExerciseDbContext>();

        services.AddScoped<IExerciseSeeder, ExerciseSeeder>();

        services.AddScoped<IExerciseRepository, ExerciseRepository>();
    }
}
