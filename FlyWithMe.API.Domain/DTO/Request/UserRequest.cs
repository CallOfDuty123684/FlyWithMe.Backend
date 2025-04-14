namespace FlyWithMe.API.Domain.DTO.Request
{
    public class UserRequest
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
