using AutoMapper;
using Models.DTO;

namespace GymzzyWebAPI.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailsViewDTO>();
        }
    }
}
