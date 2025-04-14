using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Model;

namespace FlyWithMe.API.Persistence.Services
{
    public interface IChatGPTService
    {
        Task<ChatResponse> GetChatbotResponse(ChatRequest request);
    }
}
