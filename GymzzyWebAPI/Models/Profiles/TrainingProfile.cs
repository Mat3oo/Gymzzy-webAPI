using AutoMapper;
using GymzzyWebAPI.Models.DTO;

namespace GymzzyWebAPI.Models.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<TrainingCreateDTO, Training>();
            CreateMap<TrainingCreateDTO.SeriesDTO, Series>();
            CreateMap<TrainingCreateDTO.SeriesDTO.ExerciseDTO, Exercise>();

            CreateMap<Training, TrainingViewDTO>();
            CreateMap<Series, TrainingViewDTO.SeriesDTO>();
            CreateMap<Exercise, TrainingViewDTO.SeriesDTO.ExerciseDTO>();
        }
    }
}
