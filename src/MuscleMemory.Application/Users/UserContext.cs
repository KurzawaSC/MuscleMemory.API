using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MuscleMemory.Application.Users;

 public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null
            ? (DateOnly?)null
            : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");
        var weightString = user.FindFirst(c => c.Type == "Weight")?.Value;
        var heightString = user.FindFirst(c => c.Type == "Height")?.Value;
        double weight, height;
        double.TryParse(weightString, out weight);
        double.TryParse(heightString, out height);

        return new CurrentUser(userId, email, roles, nationality, dateOfBirth, weight, height);
    }
}

