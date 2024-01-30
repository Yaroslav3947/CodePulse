using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
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

        public async Task<BlogPostLike> AddLikeForBlogPost(BlogPostLike blogPostLike) {
            await _dbContext.BlogPostLike.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId) {
            return await _dbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<int> GetTotalLikesByUrlHandleAsync(string urlHandle) {

            var blogPostId = _dbContext.BlogPosts
                .Where(x => x.UrlHandle == urlHandle)
                .Select(x => x.Id).FirstOrDefault();

            return await _dbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
