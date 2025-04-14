using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithMe.API.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITourDetailsRepository _tourDetailsRepository;

        public TourController(IConfiguration config, ITourDetailsRepository tourDetailsRepository)
        {
            _config = config;
            _tourDetailsRepository = tourDetailsRepository;
        }

        /// <summary>
        /// Get Tour Details based on placeName
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        [HttpGet("tour-details")]
        [EnableCors]
        public TourDetails GetTourDetails(string placeName)
        {
            return _tourDetailsRepository.GetTourDetailsListBasedonPlace(placeName);
        }
    }
}
