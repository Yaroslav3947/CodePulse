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

        public async Task<CosmeticLike> AddCosmeticToBasket(CosmeticLike cosmeticLike) {
            await _dbContext.CosmeticLikes.AddAsync(cosmeticLike);
            await _dbContext.SaveChangesAsync();
            return cosmeticLike;
        }

        public async Task DeleteAsync(Guid cosmeticId) {
            var cosmeticLikes = await _dbContext.CosmeticLikes
            .Where(x => x.CosmeticId == cosmeticId).ToListAsync();

            _dbContext.CosmeticLikes.RemoveRange(cosmeticLikes);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CosmeticLike> RemoveCosmeticFromBasket(CosmeticLike cosmeticLike) {
            var existingLike = await _dbContext.CosmeticLikes.FirstOrDefaultAsync(x => x.UserId == cosmeticLike.UserId);

            if(existingLike != null) {
                _dbContext.CosmeticLikes.Remove(existingLike);
                await _dbContext.SaveChangesAsync();
            }
            return cosmeticLike;
        }
    }
}
