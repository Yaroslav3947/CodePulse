using CodePulse.API.Models.Domain;
using System.Collections;
using System.Net;

namespace CodePulse.API.Repositories.Interface {
    public interface IImageRepository {
        Task<ProductImage> Upload(IFormFile file, ProductImage image);
        Task<IEnumerable<ProductImage>> GetAll();
    }
}
