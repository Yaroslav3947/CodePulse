using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repositories.Interface {
    public interface IUsersRepository {
        Task<IEnumerable<IdentityUser>> GetUsersAsync();
    }
}
