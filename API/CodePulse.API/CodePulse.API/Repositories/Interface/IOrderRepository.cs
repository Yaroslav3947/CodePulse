using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(Guid id);

        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);

        Task<Order?> UpdateAsync(Order order);

        Task<Order?> DeleteAsync(Guid id);
    }
}
