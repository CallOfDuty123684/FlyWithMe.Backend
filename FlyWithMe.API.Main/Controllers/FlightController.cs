using FlyWithMe.API.Domain.DTO.Request;
using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using FlyWithMe.API.Persistence.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithMe.API.Main.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IAmadeusService _amadeusService;

        public FlightController(IAmadeusService amadeusService)
        {
            _amadeusService = amadeusService;
        }

        /// <summary>
        /// Get Flights based on FlightRequest
        /// </summary>
        /// <param name="flightRequest"></param>
        /// <returns></returns>
        [HttpPost("GetFlights")]
        [EnableCors]
        public async Task<FlightResponse> GetFlights(FlightRequest flightRequest)
        {
            return await _amadeusService.GetFlightsAsync(flightRequest);
        }
    }
}
