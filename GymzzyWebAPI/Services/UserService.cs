using AutoMapper;
using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Services.Interfaces;
using Models.DTO;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id)
        {
            var temp = await _unitOfWork.Users.GetAsync(id);

            UserDetailsViewDTO userDTO = null;

            if (temp != null)
            {
                userDTO = _mapper.Map<UserDetailsViewDTO>(temp);
            }

            return userDTO;
        }
    }
}
