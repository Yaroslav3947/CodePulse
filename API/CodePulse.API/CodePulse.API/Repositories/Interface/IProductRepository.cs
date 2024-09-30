using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IProductRepository {
        Task<Product> CreateAsync(Product cosmetic);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Product?> GetByUrlHandleAsync(string urlHandle);
        Task<Product?> UpdateAsync(Product cosmetic);

        Task<Product?> DeleteAsync(Guid id);
    }
}
