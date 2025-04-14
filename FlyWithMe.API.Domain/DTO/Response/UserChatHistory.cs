namespace FlyWithMe.API.Domain.DTO.Response
{
    public class UserChatHistory 
    {
        public long ChatId { get; set; }

        public long UserId { get; set; }

        public string UserChatRequest { get; set; }

        public string UserChatResponse { get; set; }

        public string CreatedDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public ChatResponse chatResponse { get; set; }
        public UserChatHistory()
        {
            chatResponse = new ChatResponse();
        }
    }
}
