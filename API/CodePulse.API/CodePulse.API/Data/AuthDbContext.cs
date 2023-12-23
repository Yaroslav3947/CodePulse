using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data {
    public class AuthDbContext : IdentityDbContext {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var readerRoleId = "b3e7cecb-9a46-40f9-bbcc-a23760632341";
            var writerRoleId = "29b1b37d-9ecd-4726-9b87-8b546903520d";

            // Create reader and writer roles
            var roles = new List<IdentityRole> {
                new IdentityRole() {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole() {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };
            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = "f51c9878-d4ab-4030-8088-8e6350383632";
            var admin = new IdentityUser() {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>() {
                new() {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new() {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


        }
    }
}
