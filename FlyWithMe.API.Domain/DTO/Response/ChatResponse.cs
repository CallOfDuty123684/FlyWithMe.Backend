namespace FlyWithMe.API.Domain.DTO.Response
{
    public class ChatResponse
    {
        public string ChatResult { get; set; }
        public List<ItineraryDay> JsonItinerary { get; set; }
    }
}
