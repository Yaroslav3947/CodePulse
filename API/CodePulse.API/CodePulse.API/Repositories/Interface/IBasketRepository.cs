using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IBasketRepository {
        Task<ProductLike> AddProductToBasket(ProductLike productLike);
        Task<ProductLike> RemoveProductFromBasket(ProductLike productLike);
        Task DeleteAsync(Guid productId);
    }
}
