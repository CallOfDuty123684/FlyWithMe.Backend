using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithMe.API.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : Controller
    {
        private readonly IAmadeusService _amadeusService;

        public HotelController(IAmadeusService amadeusService)
        {
            _amadeusService = amadeusService;
        }

        /// <summary>
        /// Get Hotels based on HotelRequest
        /// </summary>
        /// <param name="hotelRequest"></param>
        /// <returns></returns>
        [HttpPost("GetHotels")]
        [EnableCors]
        public async Task<HotelOffersResponse> GetHotelsAsync(HotelRequest hotelRequest)
        {
            return await _amadeusService.GetHotelsAsync(hotelRequest);
        }
    }
}
