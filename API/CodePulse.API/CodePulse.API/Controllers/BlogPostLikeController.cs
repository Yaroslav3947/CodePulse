using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository) {
            this._blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] BlogLikeDto blogLikeDto) {

            var response = new BlogPostLike {
                BlogPostId = blogLikeDto.BlogPostId,
                UserId = blogLikeDto.UserId
            };

            await _blogPostLikeRepository.AddLikeForBlogPost(response);

            return Ok(response);
        }
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveLike([FromBody] BlogLikeDto blogLikeDto) {

            var response = new BlogPostLike {
                BlogPostId = blogLikeDto.BlogPostId,
                UserId = blogLikeDto.UserId
            };

            await _blogPostLikeRepository.RemoveLikeForBlogPost(response);

            return Ok(response);
        }
    }
}
