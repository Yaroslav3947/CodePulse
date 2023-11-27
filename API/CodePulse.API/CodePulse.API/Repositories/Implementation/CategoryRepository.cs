using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class CategoryRepository : ICategoryRepository {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category) {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id) {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
