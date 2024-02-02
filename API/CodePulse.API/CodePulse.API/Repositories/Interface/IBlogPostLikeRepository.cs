using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IBlogPostLikeRepository {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlogPost(BlogPostLike blogPostLike);
        Task<int> GetTotalLikesByUrlHandleAsync(string urlHandle);
        Task<IEnumerable<Guid>> GetUsersLikingBlogPostByIdAsync(Guid blogPostId);
    }
}
