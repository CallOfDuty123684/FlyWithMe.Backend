using FlyWithMe.API.Persistence.Services;
using Microsoft.Graph.Models;
using OpenAI.Chat;
using OpenAI;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Model;
using System.Net.Http.Headers;
using FlyWithMe.API.Application.Helpers;
using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Persistence.Models;
using System.Text.RegularExpressions;

public class ChatGPTService : IChatGPTService
{
    private readonly OpenAIClient _openAiClient;
    private readonly IConfiguration _configuration;
    private readonly string _openAIKey = "";
    private readonly string _openAIApiUrl = "";
    private readonly string _unsplashSecret = "";
    private readonly FlyWithMeContext _context;

    public ChatGPTService(IConfiguration configuration, FlyWithMeContext context)
    {
        _configuration = configuration;
        _openAIKey = configuration["OpenAIKey"];
        _openAIApiUrl = configuration["OpenAIAPIKey"];
        _context = context;
        _unsplashSecret = configuration["UnsplashSecretKey"];
    }

    public async Task<ChatResponse> GetChatbotResponse(ChatRequest request)
    {
        ChatResponse result = new ChatResponse();
        try
        {
            string initialMessage = request.Message;
            string chatGptMessage = string.Empty;

            // Check if message contains travel-related words
            bool isTravelRequest = ContainsTravelRequest(initialMessage);
            bool isGreetingOnly = IsGreetingOnly(initialMessage);

            // Extract the number of days requested
            int requestedDays = ExtractTripDays(request.Message);

            if (requestedDays > 0)
            {
                chatGptMessage = string.Format(Constants.OpenAIInstructions.TravelItineraryPrompt, requestedDays);
            }

            // If it's only a greeting, return a friendly response
            if (!isTravelRequest && isGreetingOnly)
            {
                result.ChatResult = "Hello! How can I assist you with your travel plans today? 😊";
                return result;
            }

            if (isTravelRequest)
            {
                var requestBody = new
                {
                    model = "gpt-4-turbo",
                    messages = new[]
    {
        new { role = "user", content = request.Message + chatGptMessage }
    }
                };


                string json = JsonSerializer.Serialize(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                int retryCount = 0;
                int maxRetries = 3;
                int delay = 2000;

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAIKey);

                while (retryCount < maxRetries)
                {


                    HttpResponseMessage response = await client.PostAsync(_openAIApiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        using var doc = JsonDocument.Parse(responseContent);
                        var choices = doc.RootElement.GetProperty("choices");

                        string reply = choices[0].GetProperty("message").GetProperty("content").GetString();

                        // Extract JSON block from markdown ```json ... ```
                        int jsonStart = reply.IndexOf("```json");
                        int jsonEnd = reply.LastIndexOf("```");

                        string jsonResponse = "";
                        string chatDescription = reply; // Default: Use full response if JSON is missing

                        if (jsonStart != -1 && jsonEnd != -1 && jsonEnd > jsonStart)
                        {
                            jsonResponse = reply.Substring(jsonStart + 7, jsonEnd - (jsonStart + 7)).Trim();
                            chatDescription = reply.Substring(0, jsonStart).Trim(); // Use only text before JSON
                        }
                        else
                        {
                            // Fallback: If JSON block is missing, try to extract after "JSON Response:"
                            int jsonMarker = reply.LastIndexOf("JSON Response:");
                            if (jsonMarker != -1)
                            {
                                chatDescription = reply.Substring(0, jsonMarker).Trim();
                                string possibleJson = reply.Substring(jsonMarker + 14).Trim();

                                if (!string.IsNullOrEmpty(possibleJson) && possibleJson.StartsWith("{"))
                                {
                                    jsonResponse = possibleJson;
                                }
                            }
                        }

                        // Parse JSON itinerary
                        if (!string.IsNullOrEmpty(jsonResponse))
                        {
                            try
                            {
                                result.JsonItinerary = JsonSerializer.Deserialize<List<ItineraryDay>>(jsonResponse);
                                // Ensure the itinerary matches the requested days
                                if (requestedDays > 0 && result.JsonItinerary.Count < requestedDays)
                                {
                                    retryCount++;
                                    if (retryCount < maxRetries)
                                    {
                                        await Task.Delay(delay);
                                        continue;  // Retry the request
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                result.JsonItinerary = new List<ItineraryDay>();
                            }
                        }
                        else
                        {
                            result.JsonItinerary = null;
                        }

                        // Ensure ChatResult is engaging
                        if (result.JsonItinerary != null && result.JsonItinerary.Any())
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine("🌍 Your exciting travel itinerary is ready! Here's how your trip unfolds:");

                            foreach (var item in result.JsonItinerary)
                            {
                                string imageUrl = await GetUnsplashImage(item.Location);
                                item.ImageUrl = imageUrl ?? "https://via.placeholder.com/150";

                                sb.AppendLine($"\n➡️ On your journey, you'll visit **{item.Location}**, {item.Description}");
                            }

                            chatDescription = sb.ToString();
                        }

                        result.ChatResult = chatDescription;

                        // Save chat history (optional)
                        if (request.UserId != null && request.UserId > 0)
                        {
                            Userchatdetail userChatDetailsRequest = new Userchatdetail
                            {
                                Userchatrequest = initialMessage,
                                Userchatresponse = chatDescription, // Save enhanced ChatResult
                                Userid = (long)request.UserId,
                                Createddate = DateTime.Now
                            };
                            _context.Userchatdetails.Add(userChatDetailsRequest);
                            await _context.SaveChangesAsync();
                        }

                        return result;
                    }

                    retryCount++;
                    await Task.Delay(delay);
                }

                result.ChatResult = "Request failed after multiple attempts.";
            }
            else
            {
                var requestBody = new
                {
                    model = "gpt-4-turbo",
                    messages = new[]
               {
                new { role = "user", content = request.Message + Constants.OpenAIInstructions.NonTravelItineraryPrompt }
            }
                };

                string json = JsonSerializer.Serialize(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAIKey);
                HttpResponseMessage response = await client.PostAsync(_openAIApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    using var doc = JsonDocument.Parse(responseContent);
                    var choices = doc.RootElement.GetProperty("choices");

                    result.ChatResult = choices[0].GetProperty("message").GetProperty("content").GetString();
                    result.JsonItinerary = new List<ItineraryDay>();
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            result.ChatResult = "Sorry, I am having trouble understanding your request.";
        }

        return result;
    }
    public bool IsGreetingOnly(string userInput)
    {
        string[] greetings = { "hi", "hello", "hey", "good morning", "good evening", "good afternoon" };

        // Check if the message consists ONLY of a greeting (not mixed with travel keywords)
        return greetings.Contains(userInput.Trim().ToLower());
    }

    public bool ContainsTravelRequest(string userInput)
    {
        string[] travelKeywords = { "trip", "itinerary", "plan", "vacation", "travel", "places", "destination" };

        // Convert input to lowercase and check if it contains travel-related words
        return travelKeywords.Any(keyword => userInput.ToLower().Contains(keyword));
    }


    private async Task<string> GetUnsplashImage(string locationName)
    {
        string unsplashUrl = $"https://api.unsplash.com/search/photos?query={Uri.EscapeDataString(locationName)}&client_id={_unsplashSecret}&per_page=1";

        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(unsplashUrl);
        if (!response.IsSuccessStatusCode) return null;

        string jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        var root = doc.RootElement;

        if (root.TryGetProperty("results", out JsonElement results) && results.GetArrayLength() > 0)
        {
            string imageUrl = results[0].GetProperty("urls").GetProperty("regular").GetString();
            return imageUrl;
        }

        return null;
    }

    private int ExtractTripDays(string userMessage)
    {
        var numberWords = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
        { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }, { "ten", 10 },
        { "eleven", 11 }, { "twelve", 12 }, { "thirteen", 13 }, { "fourteen", 14 },
        { "fifteen", 15 }
    };

        var match = Regex.Match(userMessage, @"\b(\d+)\s*(days?|nights?)\b", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            return int.Parse(match.Groups[1].Value);
        }

        match = Regex.Match(userMessage, @"\b(one|two|three|four|five|six|seven|eight|nine|ten|eleven|twelve|thirteen|fourteen|fifteen)\s*(days?|nights?)\b", RegexOptions.IgnoreCase);
        if (match.Success && numberWords.TryGetValue(match.Groups[1].Value.ToLower(), out int dayCount))
        {
            return dayCount;
        }

        return 0; 
    }


}
