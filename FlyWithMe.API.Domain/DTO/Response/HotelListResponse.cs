using System.Text.Json.Serialization;

namespace FlyWithMe.API.Domain.DTO.Response
{
    public class HotelListResponse
    {
        [JsonPropertyName("data")]
        public List<HotelData> Data { get; set; }
    }

    public class HotelData
    {
        [JsonPropertyName("hotelId")]
        public string HotelId { get; set; }
    }

}
