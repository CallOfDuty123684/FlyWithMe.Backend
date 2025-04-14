namespace FlyWithMe.API.Domain.DTO.Response
{
    public class TourDetailsResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PlaceName { get; set; }
    }

    public class TourDetails 
    { 
    
        public string PlaceName { get; set; }
        public string VideoURL { get; set; }
        public string MainDescription { get; set; }
        public List<TourDetailsResponse> tourDetails { get; set; }
        public TourDetails()
        {
            tourDetails = new List<TourDetailsResponse>();
        }
    }
}
