using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostCommentRepository : IBlogPostCommentRepository {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostCommentRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<BlogPostComment> AddCommentForBlogPost(BlogPostComment comment) {
            await _dbContext.BlogPostComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteAsync(Guid blogPostId) {
            var blogPostComments = await _dbContext.BlogPostComments
                .Where(x => x.BlogPostId == blogPostId).ToListAsync();

            _dbContext.BlogPostComments.RemoveRange(blogPostComments);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostComment>> GetBlogPostCommentsByIdAsync(Guid blogPostId) {
            return await _dbContext.BlogPostComments.
                Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
