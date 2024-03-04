using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
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

        public async Task<IdentityUser?> UpdateAsync(IdentityUser user) {
            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if(existingUser != null) {
                _authDbContext.Entry(existingUser).CurrentValues.SetValues(user);
                await _authDbContext.SaveChangesAsync();
                return existingUser;
            }

            return null;
        }

        public async Task<IdentityUser?> DeleteAsync(Guid id) {

            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

            if(existingUser is null) {
                return null;
            }

            _authDbContext.Users.Remove(existingUser);
            await _authDbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<IdentityUser?> GetUserByIdAsync(Guid id) {
            return await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());
        }
    }
}
