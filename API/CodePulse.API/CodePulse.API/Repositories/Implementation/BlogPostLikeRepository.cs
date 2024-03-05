using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostLikeRepository : IBlogPostLikeRepository {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostLikeRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlogPost(BlogPostLike blogPostLike) {
            await _dbContext.BlogPostLike.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task DeleteAsync(Guid blogPostId) {
            var blogPostLikes = await _dbContext.BlogPostLike
            .Where(x => x.BlogPostId == blogPostId).ToListAsync();

            _dbContext.BlogPostLike.RemoveRange(blogPostLikes);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetUsersLikingBlogPostByIdAsync(Guid blogPostId) {

            var usersLikingBlogPost = await _dbContext.BlogPostLike
                .Where(x => x.BlogPostId == blogPostId)
                .Select(x => x.UserId)
                .ToListAsync();

            return usersLikingBlogPost;
        }

        public async Task<BlogPostLike> RemoveLikeForBlogPost(BlogPostLike blogPostLike) {
            var existingLike = await _dbContext.BlogPostLike.FirstOrDefaultAsync(x => x.UserId == blogPostLike.UserId);

            if(existingLike != null) {
                _dbContext.BlogPostLike.Remove(existingLike);
                await _dbContext.SaveChangesAsync();
            }
            return blogPostLike;
        }
    }
}
