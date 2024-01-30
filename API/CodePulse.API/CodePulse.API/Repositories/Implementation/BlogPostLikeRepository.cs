using CodePulse.API.Data;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostLikeRepository : IBlogPostLikeRepository {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostLikeRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<int> GetTotalLikes(Guid blogPostId) {
            return await _dbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
