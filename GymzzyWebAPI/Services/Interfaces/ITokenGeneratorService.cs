using GymzzyWebAPI.Models;

namespace GymzzyWebAPI.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(User user);
    }
}
