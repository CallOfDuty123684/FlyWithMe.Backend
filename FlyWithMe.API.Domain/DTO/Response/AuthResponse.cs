using System.Text.Json.Serialization;

namespace FlyWithMe.API.Domain.DTO.Response
{
    public class AuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
