using System.Text.Json.Serialization;

namespace FlyWithMe.API.Domain.DTO.Response
{
    public class FlightResponse  : ReturnResponse
    {
        [JsonPropertyName("data")]
        public List<FlightOffer> Data { get; set; }
    }

    public class FlightOffer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("itineraries")]
        public List<Itinerary> Itineraries { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }

        [JsonPropertyName("validatingAirlineCodes")]
        public List<string> ValidatingAirlineCodes { get; set; }
    }

    public class Itinerary
    {
        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("segments")]
        public List<Segment> Segments { get; set; }
    }

    public class Segment
    {
        [JsonPropertyName("departure")]
        public AirportInfo Departure { get; set; }

        [JsonPropertyName("arrival")]
        public AirportInfo Arrival { get; set; }

        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonPropertyName("number")]
        public string FlightNumber { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("durationTime")]
        public string DurationTime { get; set; }
    }

    public class AirportInfo
    {
        [JsonPropertyName("iataCode")]
        public string IataCode { get; set; }

        [JsonPropertyName("airportName")]
        public string AirportName { get; set; } 

        [JsonPropertyName("at")]
        public string DepartureTime { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("total")]
        public string Total { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

}
