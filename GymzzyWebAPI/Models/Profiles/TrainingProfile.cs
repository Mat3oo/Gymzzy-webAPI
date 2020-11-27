using AutoMapper;
using GymzzyWebAPI.Models.DTO;

namespace GymzzyWebAPI.Models.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<Training, TrainingSimpleViewDTO>();

            CreateMap<TrainingCreateDTO, Training>();
            CreateMap<TrainingCreateDTO.TrainingCreateSeriesDTO, Series>();
            CreateMap<TrainingCreateDTO.TrainingCreateSeriesDTO.TrainingCreateExerciseDTO, Exercise>();

            CreateMap<TrainingEditDTO, Training>();
            CreateMap<TrainingEditDTO.TrainingEditSeriesDTO, Series>();
            CreateMap<TrainingEditDTO.TrainingEditSeriesDTO.TrainingEditExerciseDTO, Exercise>();

            CreateMap<Training, TrainingViewDTO>();
            CreateMap<Series, TrainingViewDTO.TrainingViewSeriesDTO>();
            CreateMap<Exercise, TrainingViewDTO.TrainingViewSeriesDTO.TrainingViewExerciseDTO>();
        }
    }
}
