using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface {
    public interface IProductLikeRepository {
        Task<ProductLike> AddLikeForProduct(ProductLike productLike);

        Task<ProductLike> RemoveLikeForProduct(ProductLike productLike);
        Task<IEnumerable<Guid>> GetUsersLikingProductByIdAsync(Guid productId);
        Task DeleteAsync(Guid productId);
    }
}
