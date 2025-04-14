using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;

namespace FlyWithMe.API.Persistence.Services
{
    public interface IAmadeusService
    {
        Task<FlightResponse> GetFlightsAsync(FlightRequest flightRequest);
        Task<HotelOffersResponse> GetHotelsAsync(HotelRequest hotelRequest);
    }
}
