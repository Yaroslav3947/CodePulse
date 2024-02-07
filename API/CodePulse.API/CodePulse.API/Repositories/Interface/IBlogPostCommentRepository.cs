using CodePulse.API.Models.Domain;


namespace CodePulse.API.Repositories.Interface {
    public interface IBlogPostCommentRepository {
        Task<BlogPostComment> AddCommentForBlogPost(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetBlogPostCommentsByIdAsync(Guid blogPostId);
    }
}
