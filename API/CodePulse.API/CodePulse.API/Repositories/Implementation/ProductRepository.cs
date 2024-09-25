using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class ProductRepository : IProductRepository {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product) {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id) {
            var existingProduct = await _dbContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

            if(existingProduct is null) {
                return null;
            }

            _dbContext.Products.Remove(existingProduct);

            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() {
            return await _dbContext.Products.Include(x => x.Categories).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id) {
            return await _dbContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetByUrlHandleAsync(string urlHandle) {
            return await _dbContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Product?> UpdateAsync(Product product) {
            var existingProduct =  await _dbContext.Products.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == product.Id);

            if(existingProduct != null) {
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                existingProduct.Categories = product.Categories;
                await _dbContext.SaveChangesAsync();
                return product;
            }

            return null;
        }
    }
}
