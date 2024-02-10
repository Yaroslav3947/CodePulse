using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class BlogPostRepository : IBlogPostRepository {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogPostRepository(ApplicationDbContext dbContext,
            IBlogPostLikeRepository blogPostLikeRepository,
            IBlogPostCommentRepository blogPostCommentRepository) {
            this._dbContext = dbContext;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._blogPostCommentRepository = blogPostCommentRepository;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost) {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id) {
            var existingBlogPost = await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

            if(existingBlogPost is null) {
                return null;
            }

            await _blogPostLikeRepository.DeleteAsync(id);
            await _blogPostCommentRepository.DeleteAsync(id);

            _dbContext.BlogPosts.Remove(existingBlogPost);

            await _dbContext.SaveChangesAsync();

            return existingBlogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync() {
            return await _dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id) {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle) {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost) {
            var existingBlogPost =  await _dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            // TODO: also update likes and comments
            if(existingBlogPost != null) {
                _dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
                existingBlogPost.Categories = blogPost.Categories;
                await _dbContext.SaveChangesAsync();
                return blogPost;
            }

            return null;
        }
    }
}
