using System.Reflection.Metadata;

namespace FlyWithMe.API.Domain.DTO.Response
{
    public class UserAuthenticationResponse
    {
        public long? UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName { get; set; }
        public string Token { get; set; }
    }
}
