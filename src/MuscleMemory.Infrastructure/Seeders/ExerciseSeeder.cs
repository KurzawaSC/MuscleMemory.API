using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MuscleMemory.Domain.Constants;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Infrastructure.Presistence;

namespace MuscleMemory.Infrastructure.Seeders;

internal class ExerciseSeeder(ExerciseDbContext dbContext,
    UserManager<User> userManager,
    IConfiguration configuration) : IExerciseSeeder
{
    private readonly string AdminEmail = "admin@admin.com";
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        { 
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            var admin = await userManager.FindByEmailAsync(AdminEmail);

            if(admin == null)
            {
                var newAdmin = CreateAdmin();

                await userManager.CreateAsync(newAdmin, configuration["AppSettings:SeedAdminPassword"]!);

                await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
            }
        }

    }
    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.UserPremium)
                {
                    NormalizedName = UserRoles.UserPremium.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];
        return roles;
    }

    private User CreateAdmin()
    {
        User admin = new()
        {
            UserName = AdminEmail,
            Email = AdminEmail,
            UsersExercices = [],
        };

        return admin;
    }
}
