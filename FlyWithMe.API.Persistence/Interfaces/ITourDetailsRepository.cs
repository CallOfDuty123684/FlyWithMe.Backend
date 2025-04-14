using FlyWithMe.API.Domain.DTO.Response;

namespace FlyWithMe.API.Persistence.Interfaces
{
    public interface ITourDetailsRepository
    {
        TourDetails GetTourDetailsListBasedonPlace(string placeName);
    }
}
