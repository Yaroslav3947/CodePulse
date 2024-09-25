using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteAsync(Guid id)
        {
            var existingOrder = await _dbContext.Orders
                .Include(x => x.Products) // Assuming Order has a collection of Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrder is null)
            {
                return null;
            }

            _dbContext.Orders.Remove(existingOrder);
            await _dbContext.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Include(x => x.Products) // Assuming Order has a collection of Products
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                .Include(x => x.Products) // Assuming Order has a collection of Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Orders
                .Where(x => x.UserId == userId) // Assuming Order has a UserId property
                .Include(x => x.Products) // Assuming Order has a collection of Products
                .ToListAsync();
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            var existingOrder = await _dbContext.Orders
                .Include(x => x.Products) // Assuming Order has a collection of Products
                .FirstOrDefaultAsync(x => x.Id == order.Id);

            if (existingOrder != null)
            {
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
                existingOrder.Products = order.Products; // Assuming Order has a collection of Products
                await _dbContext.SaveChangesAsync();
                return order;
            }

            return null;
        }
    }
}
