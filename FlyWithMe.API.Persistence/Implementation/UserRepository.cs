using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using FlyWithMe.API.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using System.Text.Json;

namespace FlyWithMe.API.Persistence.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly FlyWithMeContext _context;
        public UserRepository(FlyWithMeContext context)
        {
            _context = context;
        }

        public async Task<long> SaveUserDetails(UserRequest request)
        {
            long returnValue = 0;
            if (request != null)
            {
                try
                {
                    // check if email address already exists in the table
                    var userDetails = await _context.Usermasters.Where(x => x.Emailid.ToLower() == request.EmailId.ToLower()).FirstOrDefaultAsync();
                    if (userDetails != null)
                    {
                        userDetails.Fullname = request.FullName;
                        userDetails.Lastlogindate = DateTime.Now;
                        userDetails.Firstname = request.FirstName;
                        userDetails.Lastname = request.LastName;
                        _context.Usermasters.Update(userDetails);
                        await _context.SaveChangesAsync();
                        returnValue = userDetails.Userid;
                    }
                    else
                    {
                        Usermaster user = new Usermaster
                        {
                            Firstname = request.FirstName,
                            Lastname = request.LastName,
                            Fullname = request.FullName,
                            Emailid = request.EmailId,
                            Createdon = DateTime.Now,
                            Lastlogindate = DateTime.Now
                        };
                        _context.Usermasters.Add(user);
                        await _context.SaveChangesAsync();
                        returnValue = user.Userid;
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return returnValue;
        }

        public async Task<List<UserChatHistory>> GetUserChatHistory(GetUserId request)
        {
            List<UserChatHistory> userChatHistories = new List<UserChatHistory>();
            if (request != null)
            {
                userChatHistories = await _context.Userchatdetails.Where(x => x.Userid == request.UserId)
                    .Select(a => new UserChatHistory
                    {
                        ChatId = a.Chatid,
                        UserId = a.Userid,
                        UserChatRequest = a.Userchatrequest,
                        UserChatResponse = a.Userchatresponse,
                        CreatedDate = a.Createddate.Value.ToString("dd-MM-yyyy"),
                        CreatedOn = a.Createddate
                    }).ToListAsync();

                userChatHistories = userChatHistories.OrderByDescending(x => x.CreatedOn).ToList();

                if (userChatHistories != null && userChatHistories.Any())
                {
                    foreach (var a in userChatHistories)
                    {
                        a.chatResponse = await DeserializeChatResponse(a.UserChatResponse);
                    }

                }
            }
            return userChatHistories;
        }

        private async Task<ChatResponse> DeserializeChatResponse(string userChatResponse)
        {
            ChatResponse chatResponse = new ChatResponse();

            // Extract JSON block from markdown ```json ... ```
            int jsonStart = userChatResponse.IndexOf("```json");
            int jsonEnd = userChatResponse.LastIndexOf("```");

            string jsonResponse = "";
            string chatDescription = userChatResponse; // Default: Use full response if JSON is missing

            if (jsonStart != -1 && jsonEnd != -1 && jsonEnd > jsonStart)
            {
                jsonResponse = userChatResponse.Substring(jsonStart + 7, jsonEnd - (jsonStart + 7)).Trim();
                chatDescription = userChatResponse.Substring(0, jsonStart).Trim(); // Use only text before JSON
            }
            else
            {
                // Fallback: If JSON block is missing, try to extract after "JSON Response:"
                int jsonMarker = userChatResponse.LastIndexOf("JSON Response:");
                if (jsonMarker != -1)
                {
                    chatDescription = userChatResponse.Substring(0, jsonMarker).Trim();
                    string possibleJson = userChatResponse.Substring(jsonMarker + 14).Trim();

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
                    chatResponse.JsonItinerary = JsonSerializer.Deserialize<List<ItineraryDay>>(jsonResponse);
                }
                catch (Exception)
                {
                    chatResponse.JsonItinerary = new List<ItineraryDay>();
                }
            }
            else
            {
                chatResponse.JsonItinerary = null;
            }
            chatResponse.ChatResult = chatDescription;
            return chatResponse;
        }
    }
}