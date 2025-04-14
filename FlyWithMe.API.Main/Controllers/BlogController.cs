using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithMe.API.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogDetailsRepository _blogDetailsRepository;

        public BlogController(IBlogDetailsRepository IBlogDetailsRepository)
        {
            _blogDetailsRepository = IBlogDetailsRepository;
        }

        /// <summary>
        /// Get Blog Details based on BlogId
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [HttpGet("blog-details")]
        [EnableCors]
        public BlogDetails GetBlogDetails(string blogId)
        {
            return _blogDetailsRepository.GetBlogDetailsListBasedonBlogId(blogId);
        }
    }
}
