using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repositories.Interface {
    public interface IUsersRepository {
        Task<IEnumerable<IdentityUser>> GetUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(Guid id);
        Task<IdentityUser?> UpdateAsync(IdentityUser user);
        Task<IdentityUser?> DeleteAsync(Guid id);
    }
}
