using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IBasketRepository {
        Task<CosmeticLike> AddCosmeticToBasket(CosmeticLike cosmeticLike);
        Task<CosmeticLike> RemoveCosmeticFromBasket(CosmeticLike cosmeticLike);
        Task DeleteAsync(Guid cosmeticId);
    }
}
