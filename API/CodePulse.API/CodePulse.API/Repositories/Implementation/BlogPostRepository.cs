using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostRepository : IBlogPostRepository {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;

        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost) {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync() {
            return await _dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}
