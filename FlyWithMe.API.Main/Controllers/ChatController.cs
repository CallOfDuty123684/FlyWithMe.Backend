using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using FlyWithMe.API.Persistence.Model;
using FlyWithMe.API.Persistence.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithMe.API.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatGPTService _chatGPTService1;
        private readonly IUserRepository _userRepository;
        public ChatController(IChatGPTService chatGPTService, IUserRepository userRepository)
        {
            _chatGPTService1 = chatGPTService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get Chat Response
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors]
        public async Task<ChatResponse> GetChatResponse(ChatRequest form)
        {
            var result = await _chatGPTService1.GetChatbotResponse(form);
            return result;
        }

        /// <summary>
        /// To get chat history of user based on user id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetUserChatHistory")]
        [HttpPost]
        [EnableCors]
        public async Task<List<UserChatHistory>> GetUserChatHistory(GetUserId request)
        {
            return await _userRepository.GetUserChatHistory(request);
        }
    }
}
