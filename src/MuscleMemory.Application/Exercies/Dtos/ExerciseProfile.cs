using AutoMapper;
using MuscleMemory.Application.Exercies.Commands.CreateExercise;
using MuscleMemory.Application.Exercies.Commands.UpdateRecord;
using MuscleMemory.Domain.Entities;
using System.Net;

namespace MuscleMemory.Application.Exercies.Dtos;

public class ExerciseProfile : Profile
{ 
    public ExerciseProfile()
    {

        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(dest => dest.Record, opt => opt.MapFrom(src => $"{src.Weight}x{src.Reps}"));
        CreateMap<Exercise, ExerciseDto>();

    }
}
