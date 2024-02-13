using CodePulse.API.Data;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation {
    public class UsersRepository : IUsersRepository {
        private readonly AuthDbContext _authDbContext;

        public UsersRepository(AuthDbContext authDbContext) {
            this._authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetUsersAsync() {
            var users = await _authDbContext.Users.ToListAsync();

            var adminUser = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@codepulse.com");

            if(adminUser is not null) {
                users.Remove(adminUser);
            }

            return users;
        }
    }
}
