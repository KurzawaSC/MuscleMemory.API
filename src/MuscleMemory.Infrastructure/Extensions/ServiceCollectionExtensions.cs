using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MuscleMemory.Infrastructure.Presistence;
using MuscleMemory.Infrastructure.Repositories;
using MuscleMemory.Infrastructure.Seeders;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MuscleMemory.Infrastructure.Configuration;
using MuscleMemory.Domain.Interfaces;
using MuscleMemory.Infrastructure.Storage;
using MuscleMemory.Infrastructure.Authorization.Services;


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

        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddScoped<IExerciseAuthorizationService, ExerciseAuthorizationService>();
    }
}
