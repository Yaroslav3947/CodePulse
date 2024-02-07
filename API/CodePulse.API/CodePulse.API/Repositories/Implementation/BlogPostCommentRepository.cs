using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostCommentRepository : IBlogPostCommentRepository {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostCommentRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<BlogPostComment>> GetBlogPostCommentsByIdAsync(Guid blogPostId) {
            return await _dbContext.BlogPostComments.
                Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
