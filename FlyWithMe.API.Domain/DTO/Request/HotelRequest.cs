namespace FlyWithMe.API.Domain.DTO.Request
{
    public class HotelRequest
    {
        public string City { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int AdultCount { get; set; }
    }

}
