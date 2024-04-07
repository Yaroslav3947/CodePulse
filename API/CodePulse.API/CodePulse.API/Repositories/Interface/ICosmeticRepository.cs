using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface ICosmeticRepository {
        Task<Cosmetic> CreateAsync(Cosmetic cosmetic);

        Task<IEnumerable<Cosmetic>> GetAllAsync();

        Task<Cosmetic> GetByIdAsync(Guid id);
        Task<Cosmetic?> GetByUrlHandleAsync(string urlHandle);
        Task<Cosmetic?> UpdateAsync(Cosmetic cosmetic);

        Task<Cosmetic?> DeleteAsync(Guid id);
    }
}
