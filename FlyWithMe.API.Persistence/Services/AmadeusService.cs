using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.DeviceManagement.ImportedWindowsAutopilotDeviceIdentities.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlyWithMe.API.Persistence.Services
{
    public class AmadeusService : IAmadeusService
    {
        private readonly HttpClient _httpClient;
        private readonly string ApiKey = "";
        private readonly string ApiSecret = "";
        private string _accessToken;
        private readonly IConfiguration _configuration;


        public AmadeusService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            ApiKey = configuration["Amadeus:APIKey"];
            ApiSecret = configuration["Amadeus:APISecret"];
        }

        // Authenticate and get Access Token
        private async Task AuthenticateAsync()
        {
            var authUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
            var content = new StringContent($"grant_type=client_credentials&client_id={ApiKey}&client_secret={ApiSecret}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync(authUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

            _accessToken = data?.AccessToken;
        }


        #region Flight Search
        // Fetch flights from Amadeus API
        public async Task<FlightResponse> GetFlightsAsync(FlightRequest flightRequest)
        {
            FlightResponse flightResponse = new FlightResponse();
            if (string.IsNullOrEmpty(_accessToken))
            {
                await AuthenticateAsync();
            }

            // Convert origin & destination locations to airport codes
            string originCode = await GetAirportCodeAsync(flightRequest.Origin);
            string destinationCode = await GetAirportCodeAsync(flightRequest.Destination);

            // Validate if conversion was successful
            if (string.IsNullOrEmpty(originCode) || string.IsNullOrEmpty(destinationCode))
            {
                flightResponse.ReturnMessage = "Invalid city names. Please enter valid locations.";
                flightResponse.ReturnCode = 100;
                return flightResponse;
            }

            const int maxRetries = 10;
            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                var requestUrl = $"https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode={originCode}&destinationLocationCode={destinationCode}&departureDate={flightRequest.Date}&adults={flightRequest.AdultCount}&currencyCode=USD";

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never
                    };

                    flightResponse = JsonSerializer.Deserialize<FlightResponse>(responseString, options);

                    // Check if response contains valid data
                    if (flightResponse?.Data != null && flightResponse.Data.Any())
                    {
                        // Take only 5 records
                        flightResponse.Data = flightResponse.Data.Take(5).ToList();

                        // Fetch airport names and update response
                        foreach (var offer in flightResponse.Data)
                        {
                            foreach (var itinerary in offer.Itineraries)
                            {
                                foreach (var segment in itinerary.Segments)
                                {
                                    segment.Departure.AirportName = await GetAirportNameAsync(segment.Departure.IataCode);
                                    segment.Arrival.AirportName = await GetAirportNameAsync(segment.Arrival.IataCode);

                                    // Convert duration to readable format
                                    segment.DurationTime = ConvertDurationToReadable(segment.Duration);
                                }
                            }
                        }

                        return flightResponse;
                    }
                }

                retryCount++;
                int delay = (int)Math.Pow(2, retryCount) * 1000; // Exponential backoff (1s, 2s, 4s)
                await Task.Delay(delay);
            }

            // If all retries fail, return null or throw an exception
            throw new Exception("Failed to fetch flight offers after multiple attempts.");

            return null;
        }

        public async Task<string> GetAirportCodeAsync(string location)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                await AuthenticateAsync();
            }

            string requestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations?subType=AIRPORT&keyword={Uri.EscapeDataString(location)}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return null; // API call failed
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<AirportResponse>(responseString);

            // Try to get the first matching airport code
            string airportCode = data?.Data?.FirstOrDefault()?.IataCode;

            if (!string.IsNullOrEmpty(airportCode))
            {
                return airportCode;
            }

            // If no direct airport is found, try to get the nearest airport
            return await GetNearestAirportAsync(location);
        }

        public async Task<string> GetNearestAirportAsync(string city)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                await AuthenticateAsync();
            }

            // Step 1: Get Latitude & Longitude of the Location (City)
            string geoRequestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations?subType=CITY&keyword={Uri.EscapeDataString(city)}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var geoResponse = await _httpClient.GetAsync(geoRequestUrl);

            if (!geoResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var geoResponseString = await geoResponse.Content.ReadAsStringAsync();
            var geoData = JsonSerializer.Deserialize<AirportResponse>(geoResponseString);

            var cityData = geoData?.Data?.FirstOrDefault();
            if (cityData == null || cityData.GeoCode == null)
            {
                return null;
            }

            double latitude = cityData.GeoCode.Latitude;
            double longitude = cityData.GeoCode.Longitude;

            // Step 2: Find the Nearest Airport using Latitude & Longitude
            string airportRequestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations/airports?latitude={latitude}&longitude={longitude}&radius=100";

            var airportResponse = await _httpClient.GetAsync(airportRequestUrl);

            if (!airportResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var airportResponseString = await airportResponse.Content.ReadAsStringAsync();
            var airportData = JsonSerializer.Deserialize<AirportResponse>(airportResponseString);

            string nearestAirportCode = airportData?.Data?.FirstOrDefault()?.IataCode;

            if (string.IsNullOrEmpty(nearestAirportCode))
            {
                return null;
            }

            return nearestAirportCode;
        }

        public async Task<string> GetAirportNameAsync(string iataCode)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                await AuthenticateAsync();
            }

            string requestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations?subType=AIRPORT&keyword={Uri.EscapeDataString(iataCode)}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<AirportResponse>(responseString);

            return data?.Data?.FirstOrDefault()?.Name ?? iataCode; // Return name if found, else return IATA code
        }

        private string ConvertDurationToReadable(string duration)
        {
            if (string.IsNullOrEmpty(duration)) return "N/A";

            var match = Regex.Match(duration, @"PT(\d+H)?(\d+M)?");
            if (!match.Success) return "N/A";

            string hours = match.Groups[1].Value.Replace("H", "");
            string minutes = match.Groups[2].Value.Replace("M", "");

            int h = string.IsNullOrEmpty(hours) ? 0 : int.Parse(hours);
            int m = string.IsNullOrEmpty(minutes) ? 0 : int.Parse(minutes);

            return $"{h}h {m}m";
        }
        #endregion

        #region Hotel Search

        public async Task<HotelOffersResponse> GetHotelsAsync(HotelRequest hotelRequest)
        {
            HotelOffersResponse hotelResponse = new HotelOffersResponse();

            if (string.IsNullOrEmpty(_accessToken))
            {
                await AuthenticateAsync(); // Ensure authentication
            }

            // Step 1: Get city code
            string cityCode = await GetCityCodeAsync(hotelRequest.City);
            if (string.IsNullOrEmpty(cityCode))
            {
                return new HotelOffersResponse
                {
                    ReturnMessage = "Invalid city name. Please enter a valid location.",
                    ReturnCode = 100
                };
            }

            // Step 2: Get list of hotel IDs in the city
            var hotelIds = await GetHotelIdsByCityAsync(cityCode);
            if (hotelIds == null || !hotelIds.Any())
            {
                return new HotelOffersResponse
                {
                    ReturnMessage = "No hotels found for the selected city.",
                    ReturnCode = 101
                };
            }

            // Step 3: Fetch hotel offers using hotel IDs
            string hotelIdsParam = string.Join(",", hotelIds.Take(5)); // Use only 5 hotels to limit API call size
            const int maxRetries = 10;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                var requestUrl = $"https://test.api.amadeus.com/v3/shopping/hotel-offers?hotelIds={hotelIdsParam}&checkInDate={hotelRequest.CheckInDate}&checkOutDate={hotelRequest.CheckOutDate}&adults={hotelRequest.AdultCount}&currency=USD";

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            DefaultIgnoreCondition = JsonIgnoreCondition.Never
                        };

                        hotelResponse = JsonSerializer.Deserialize<HotelOffersResponse>(responseContent, options);

                        if (hotelResponse?.Data != null && hotelResponse.Data.Any())
                        {
                            return hotelResponse; // Valid response, return it
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        return new HotelOffersResponse
                        {
                            ReturnMessage = $"Error parsing hotel response: {jsonEx.Message}",
                            ReturnCode = 500
                        };
                    }
                }

                retryCount++;
                int delay = (int)Math.Pow(2, retryCount) * 1000; // Exponential backoff (1s, 2s, 4s)
                await Task.Delay(delay);
            }

            // If all retries fail, return a meaningful response
            return new HotelOffersResponse
            {
                ReturnMessage = "Failed to fetch hotel offers after multiple attempts.",
                ReturnCode = 500
            };

        }

        public async Task<List<string>> GetHotelIdsByCityAsync(string cityCode)
        {
            if (string.IsNullOrEmpty(_accessToken))
                await AuthenticateAsync();

            string requestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations/hotels/by-city?cityCode={cityCode}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            var hotelListResponse = JsonSerializer.Deserialize<HotelListResponse>(responseString);
            return hotelListResponse?.Data?.Select(h => h.HotelId).ToList() ?? new List<string>();
        }

        public async Task<string> GetCityCodeAsync(string cityName)
        {
            var requestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations?subType=CITY&keyword={cityName}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseString);
                var cityCode = json.RootElement.GetProperty("data")[0].GetProperty("iataCode").GetString();
                return cityCode;
            }

            return null;
        }

        public async Task<string> GetHotelNameAsync(string hotelId)
        {
            var requestUrl = $"https://test.api.amadeus.com/v1/reference-data/locations/hotels/{hotelId}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseString);
                var hotelName = json.RootElement.GetProperty("data").GetProperty("name").GetString();
                return hotelName;
            }

            return "Unknown Hotel";
        }

        #endregion

    }
}
