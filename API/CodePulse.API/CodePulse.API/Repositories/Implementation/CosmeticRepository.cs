using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class CosmeticRepository : ICosmeticRepository {
        private readonly ApplicationDbContext _dbContext;

        public CosmeticRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<Cosmetic> CreateAsync(Cosmetic cosmetic) {
            await _dbContext.Cosmetics.AddAsync(cosmetic);
            await _dbContext.SaveChangesAsync();
            return cosmetic;
        }

        public async Task<Cosmetic?> DeleteAsync(Guid id) {
            var existingCosmetic = await _dbContext.Cosmetics.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

            if(existingCosmetic is null) {
                return null;
            }

            _dbContext.Cosmetics.Remove(existingCosmetic);

            await _dbContext.SaveChangesAsync();

            return existingCosmetic;
        }

        public async Task<IEnumerable<Cosmetic>> GetAllAsync() {
            return await _dbContext.Cosmetics.Include(x => x.Categories).ToListAsync();
        }

        public async Task<Cosmetic?> GetByIdAsync(Guid id) {
            return await _dbContext.Cosmetics.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cosmetic?> GetByUrlHandleAsync(string urlHandle) {
            return await _dbContext.Cosmetics.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Cosmetic?> UpdateAsync(Cosmetic cosmetic) {
            var existingCosmetic =  await _dbContext.Cosmetics.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == cosmetic.Id);

            if(existingCosmetic != null) {
                _dbContext.Entry(existingCosmetic).CurrentValues.SetValues(cosmetic);
                existingCosmetic.Categories = cosmetic.Categories;
                await _dbContext.SaveChangesAsync();
                return cosmetic;
            }

            return null;
        }
    }
}
