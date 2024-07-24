using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using MuscleMemory.API.Tests;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Application.Exercies.Commands.UpdateRecord;
using MuscleMemory.Application.Exercies.Dtos;
using MuscleMemory.Application.Users;
using MuscleMemory.Domain.Entities;
using MuscleMemory.Domain.Interfaces;
using MuscleMemory.Domain.Repositories;
using MuscleMemory.Infrastructure.Seeders;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace MuscleMemory.API.Controllers.Tests;

public class ExerciseControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private readonly Mock<IExerciseRepository> _exerciseRepositoryMock = new();
    private readonly Mock<IUserContext> _userContextMock = new();
    private readonly Mock<IExerciseSeeder> _exerciseSeederMock = new();
    private readonly Mock<IBlobStorageService> _blobStorageServiceMock = new();
    public ExerciseControllerTests(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IExerciseRepository),
                                            _ => _exerciseRepositoryMock.Object));
                services.Replace(ServiceDescriptor.Scoped(typeof(IUserContext),
                    _ => _userContextMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(IExerciseSeeder),
                                            _ => _exerciseSeederMock.Object));
                services.Replace(ServiceDescriptor.Scoped(typeof(IBlobStorageService),
                                            _ => _blobStorageServiceMock.Object));
            });
        });
    }
    [Fact()]
    public async Task GetAllUserExercises_ForValidRequest_Returns200Ok()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercises = new List<Exercise>()
        {
            new(),
            new()
        };
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetAllUserExerciseAsync(currentUser.Id, "")).ReturnsAsync(exercises);
        //act
        var result = await client.GetAsync("api/exercises?searchPhrase=");

        //assert

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetUserExerciseById_ForValidRequest_Returns200Ok()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            OwnerId = "1"
        };
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetUserExerciseByIdAsync(exercise.Id)).ReturnsAsync(exercise);
        //act
        var response = await client.GetAsync($"api/exercises/{exercise.Id}");
        var exerciseDto = await response.Content.ReadFromJsonAsync<ExerciseDto>();

        //assert
        exerciseDto!.Id.Should().Be(exercise.Id);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetUserExerciseById_ForInvalidRequest_ShouldThrowNotFoundException()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            OwnerId = "1"
        };
        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetUserExerciseByIdAsync(exercise.Id))
            .ReturnsAsync((Exercise?)null);
        //act
        var response = await client.GetAsync($"api/exercises/{exercise.Id}");

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateExercise_ForValidRequest_Returns201Created()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var requestBody = new CreateExerciseCommand()
        {
             Name = "Allachy",
             Weight = 47.5,
             Reps = 8
        };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody),
                                            Encoding.UTF8, "application/json");

        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        //act
        var response = await client.PostAsync("api/exercises", content);

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdateExercise_ForValidRequest_Returns204NoContent()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            Name = "Benchpress",
            Record = "60x5",
            OwnerId = "1"
        };

        var requestBody = new UpdateExerciseCommand()
        {
            Name = "Bench Press",
            Weight = 100,
            Reps = 1
        };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody),
                                            Encoding.UTF8, "application/json");

        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetUserExerciseByIdAsync(exercise.Id)).ReturnsAsync(exercise);
        //act
        var response = await client.PatchAsync($"api/exercises/{exercise.Id}", content);

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteUserExerciseById_ForValidRequest_Returns204NoContent()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            Name = "Benchpress",
            Record = "60x5",
            OwnerId = "1"
        };


        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetUserExerciseByIdAsync(exercise.Id)).ReturnsAsync(exercise);
        //act
        var response = await client.DeleteAsync($"api/exercises/{exercise.Id}");

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UploadPicture_ForValidRequest_Returns204NoContent()
    {
        //arrange
        var client = _applicationFactory.CreateClient();
        var exercise = new Exercise()
        {
            Id = Guid.NewGuid(),
            Name = "Benchpress",
            Record = "60x5",
            OwnerId = "1",
            PictureUrl = null
        };

        var fileContent = new byte[] {};
        using var stream = new MemoryStream(fileContent);
        var streamContent = new StreamContent(stream);
        var content = new MultipartFormDataContent();

        // Add the stream content to the form content
        content.Add(streamContent, "file", "file_name.ext");

        var currentUser = new CurrentUser("1", "test@test.com", [], "USA", null, 98, 178);
        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);
        _exerciseRepositoryMock.Setup(m => m.GetUserExerciseByIdAsync(exercise.Id)).ReturnsAsync(exercise);
        //act
        var response = await client.PostAsync($"api/exercises/{exercise.Id}/picture", content);

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }
}