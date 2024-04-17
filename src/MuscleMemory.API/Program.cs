using MuscleMemory.API.Extensions;
using Serilog;
using MuscleMemory.Infrastructure.Extensions;
using MuscleMemory.Infrastructure.Seeders;
using MuscleMemory.Application.Extensions;
using MuscleMemory.Domain.Entities;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IExerciseSeeder>();

    await seeder.Seed();

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.MapIdentityApi<User>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }