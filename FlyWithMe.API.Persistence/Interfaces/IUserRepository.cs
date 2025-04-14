using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;

namespace FlyWithMe.API.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<long> SaveUserDetails(UserRequest request);
        Task<List<UserChatHistory>> GetUserChatHistory(GetUserId request);
    }
}
