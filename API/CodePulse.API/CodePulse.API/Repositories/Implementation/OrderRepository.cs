using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                .Include(x => x.OrderItems) // Include OrderItems instead of Products
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
                .Include(x => x.OrderItems) // Include OrderItems
                .ThenInclude(oi => oi.Product) // Include Product details for each OrderItem
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                .Include(x => x.OrderItems) // Include OrderItems
                .ThenInclude(oi => oi.Product) // Include Product details for each OrderItem
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Orders
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderItems) // Include OrderItems
                .ThenInclude(oi => oi.Product) // Include Product details for each OrderItem
                .ToListAsync();
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            var existingOrder = await _dbContext.Orders
                .Include(x => x.OrderItems) // Include OrderItems
                .FirstOrDefaultAsync(x => x.Id == order.Id);

            if (existingOrder != null)
            {
                // Update existing order values
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
                existingOrder.OrderItems = order.OrderItems; // Replace existing OrderItems with new ones
                await _dbContext.SaveChangesAsync();
                return existingOrder; // Return the updated order
            }

            return null;
        }

        // Example of adding product to an order
        public async Task<Order?> AddProductToOrderAsync(Guid orderId, Guid productId, int quantity)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) // Include existing OrderItems
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += quantity; // Update quantity if product already exists in order
            }
            else
            {
                // Create a new order item
                var newOrderItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price // Store the price at order creation
                };
                order.OrderItems.Add(newOrderItem); // Add new OrderItem to Order
            }

            await _dbContext.SaveChangesAsync();
            return order; // Return updated order
        }

        public async Task<Order?> UpdateProductQuantityInOrderAsync(Guid orderId, Guid productId, int newQuantity)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) // Include existing OrderItems
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingOrderItem == null) return null;

            existingOrderItem.Quantity = newQuantity; // Update the quantity

            await _dbContext.SaveChangesAsync();
            return order; // Return updated order
        }

        public async Task<Order?> RemoveProductFromOrderAsync(Guid orderId, Guid productId)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) // Include existing OrderItems
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingOrderItem == null) return null;

            order.OrderItems.Remove(existingOrderItem); // Remove the OrderItem from the Order

            await _dbContext.SaveChangesAsync();
            return order; // Return updated order
        }
    }
}
