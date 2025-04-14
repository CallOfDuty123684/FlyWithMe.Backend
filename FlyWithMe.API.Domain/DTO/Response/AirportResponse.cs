using System.Text.Json.Serialization;

namespace FlyWithMe.API.Domain.DTO.Response
{
    public class AirportResponse
    {
        [JsonPropertyName("data")]
        public List<Airport> Data { get; set; }
    }

    public class Airport
    {
        [JsonPropertyName("iataCode")]
        public string IataCode { get; set; }

        [JsonPropertyName("subType")]
        public string SubType { get; set; } 

        [JsonPropertyName("geoCode")]
        public GeoCode GeoCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

    public class GeoCode
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
