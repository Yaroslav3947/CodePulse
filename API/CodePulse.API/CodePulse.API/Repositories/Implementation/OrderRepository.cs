using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateAsync(Order order) {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteAsync(Guid id) {
            var existingOrder = await _dbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrder is null) {
                return null;
            }

            _dbContext.Orders.Remove(existingOrder);
            await _dbContext.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id) {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems) 
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order?> UpdateAsync(Guid id, string status) {
            var existingOrder = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);

            if(existingOrder != null) {
                existingOrder.Status = status; 
                await _dbContext.SaveChangesAsync();
                return existingOrder; 
            }

            return null;
        }


        public async Task<Order?> AddProductToOrderAsync(Guid orderId, Guid productId, int quantity) {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) 
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

            if (existingOrderItem != null) {
                existingOrderItem.Quantity += quantity;
            } else {
                var newOrderItem = new OrderItem {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                order.OrderItems.Add(newOrderItem);
            }

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateProductQuantityInOrderAsync(Guid orderId, Guid productId, int newQuantity) {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) 
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingOrderItem == null) return null;

            existingOrderItem.Quantity = newQuantity;

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> RemoveProductFromOrderAsync(Guid orderId, Guid productId) {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingOrderItem == null) return null;

            order.OrderItems.Remove(existingOrderItem);

            await _dbContext.SaveChangesAsync();
            return order; 
        }

        public async Task<Order?> GetByUserIdAsync(Guid userId) {
            return await _dbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

    }
}
