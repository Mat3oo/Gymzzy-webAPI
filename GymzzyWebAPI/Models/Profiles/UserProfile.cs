using AutoMapper;
using GymzzyWebAPI.Models.DTO;

namespace GymzzyWebAPI.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailsViewDTO>();
            CreateMap<UserRegistDTO, User>();
            CreateMap<UserDetailsEditDTO, User>();
        }
    }
}
