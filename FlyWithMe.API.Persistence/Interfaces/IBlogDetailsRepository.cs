using FlyWithMe.API.Domain.DTO.Response;

namespace FlyWithMe.API.Persistence.Interfaces
{
    public interface IBlogDetailsRepository
    {
        BlogDetails GetBlogDetailsListBasedonBlogId(string blogId);
    }
}
