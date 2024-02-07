using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostCommentController : ControllerBase {
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogPostCommentController(IBlogPostCommentRepository blogPostCommentRepository) {
            this._blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComment([FromBody] CommentDto addCommentRequest) {

            var response = new BlogPostComment {
                BlogPostId = addCommentRequest.BlogPostId,
                UserId = addCommentRequest.UserId,
                Description = addCommentRequest.Description,
                DateAdded = addCommentRequest.DateAdded
            };

            await _blogPostCommentRepository.AddCommentForBlogPost(response);

            return Ok(response);
        }
    }
}
