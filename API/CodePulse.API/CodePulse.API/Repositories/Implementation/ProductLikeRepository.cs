using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class ProductLikeRepository : IProductLikeRepository {
        private readonly ApplicationDbContext _dbContext;

        public ProductLikeRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<ProductLike> AddLikeForProduct(ProductLike productLike) {
            await _dbContext.ProductLikes.AddAsync(productLike);
            await _dbContext.SaveChangesAsync();
            return productLike;
        }

        public async Task DeleteAsync(Guid blogPostId) {
            var productLikes = await _dbContext.ProductLikes
            .Where(x => x.ProductId == blogPostId).ToListAsync();

            _dbContext.ProductLikes.RemoveRange(productLikes);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetUsersLikingProductByIdAsync(Guid blogPostId) {

            var usersLikingBlogPost = await _dbContext.ProductLikes
                .Where(x => x.ProductId == blogPostId)
                .Select(x => x.UserId)
            .ToListAsync();
            return usersLikingBlogPost;
        }

        public async Task<ProductLike> RemoveLikeForProduct(ProductLike productLike) {
            var existingLike = await _dbContext.ProductLikes.FirstOrDefaultAsync(x => x.UserId == productLike.UserId);

            if(existingLike != null) {
                _dbContext.ProductLikes.Remove(existingLike);
                await _dbContext.SaveChangesAsync();
            }
            return productLike;
        }
    }
}