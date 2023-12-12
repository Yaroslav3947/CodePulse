using CodePulse.API.Models.Domain;
using System.Collections;
using System.Net;

namespace CodePulse.API.Repositories.Interface {
    public interface IImageRepository {
        Task<BlogImage> Upload(IFormFile file, BlogImage image);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
