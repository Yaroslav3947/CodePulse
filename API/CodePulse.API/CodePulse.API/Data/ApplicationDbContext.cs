using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CosmeticLike> CosmeticLikes{ get; set; }
    }
}
