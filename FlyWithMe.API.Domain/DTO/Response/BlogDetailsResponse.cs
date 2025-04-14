namespace FlyWithMe.API.Domain.DTO.Response
{
    public class BlogDetailsResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BlogId { get; set; }
    }

    public class BlogDetails
    {

        public string BlogId { get; set; }
        public string VideoURL { get; set; }
        public string Title { get; set; }
        public string MainDescription { get; set; }
        public List<BlogDetailsResponse> blogDetails { get; set; }
        public BlogDetails()
        {
            blogDetails = new List<BlogDetailsResponse>();
        }
    }
}
