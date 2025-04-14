using Microsoft.AspNetCore.Mvc;
using FlyWithMe.API.Domain.DTO.Request;
using Google.Apis.Auth;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;

namespace FlyWithMe.API.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AccountController(IConfiguration config, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Google Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("google-login")]
        public async Task<UserAuthenticationResponse> GoogleLogin([FromBody] GoogleAuthRequest request)
        {
            UserAuthenticationResponse response = new UserAuthenticationResponse();
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["Google:ClientId"] }
                });

                if (payload != null)
                {
                    // Store user details in session
                    _httpContextAccessor.HttpContext.Session.SetString("UserEmail", payload.Email);
                    _httpContextAccessor.HttpContext.Session.SetString("UserFirstName", payload.GivenName);
                    _httpContextAccessor.HttpContext.Session.SetString("UserLastName", payload.FamilyName);
                    _httpContextAccessor.HttpContext.Session.SetString("UserFullName", payload.Name);
                    Console.WriteLine("Stored in Session: " + _httpContextAccessor.HttpContext.Session.GetString("UserEmail"));
                    response.IsAuthenticated = true;
                    response.UserEmail = payload.Email;
                    response.UserFullName = payload.Name;
                    response.UserFirstName = payload.GivenName;
                    response.UserLastName = payload.FamilyName;
                    response.Token = payload.JwtId;

                    //save user details to database.
                    UserRequest userRequest = new UserRequest
                    {
                        EmailId = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        FullName = payload.Name
                    };
                    long userid = await _userRepository.SaveUserDetails(userRequest);
                    response.UserId = userid;
                }
            }
            catch
            {
                response.IsAuthenticated = false;
            }
            return response;
        }

    }
}