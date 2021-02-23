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
            CreateMap<TrainingCreateDTO.TrainingCreateDTOExercise, Exercise>()
                .ForPath(m => m.ExerciseDetails.Name, p => p.MapFrom(p => p.Name));
            CreateMap<TrainingCreateDTO.TrainingCreateDTOExercise.TrainingCreateDTOSet, Set>();

            CreateMap<TrainingEditDTO, Training>();
            CreateMap<TrainingEditDTO.TrainingEditDTOExercise, Exercise>()
                .ForPath(m => m.ExerciseDetails.Name, p => p.MapFrom(p => p.Name));
            CreateMap<TrainingEditDTO.TrainingEditDTOExercise.TrainingEditDTOSet, Set>();

            CreateMap<Training, TrainingViewDTO>();
            CreateMap<Exercise, TrainingViewDTO.TrainingViewDTOExercise>()
                .ForMember(m => m.Name, p => p.MapFrom(s => s.ExerciseDetails.Name));
            CreateMap<Set, TrainingViewDTO.TrainingViewDTOExercise.TrainingViewDTOSet>();
        }
    }
}
