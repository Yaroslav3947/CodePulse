using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodePulse.API.Repositories.Implementation {
    public class BasketRepository : IBasketRepository {
        private readonly ApplicationDbContext _dbContext;

        public BasketRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<ProductLike> AddProductToBasket(ProductLike productLike) {
            await _dbContext.ProductLikes.AddAsync(productLike);
            await _dbContext.SaveChangesAsync();
            return productLike;
        }

        public async Task DeleteAsync(Guid productId) {
            var productLikes = await _dbContext.ProductLikes
            .Where(x => x.ProductId == productId).ToListAsync();

            _dbContext.ProductLikes.RemoveRange(productLikes);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductLike> RemoveProductFromBasket(ProductLike productLike) {
            var existingLike = await _dbContext.ProductLikes.FirstOrDefaultAsync(x => x.UserId == productLike.UserId);

            if(existingLike != null) {
                _dbContext.ProductLikes.Remove(existingLike);
                await _dbContext.SaveChangesAsync();
            }
            return productLike;
        }
    }
}
