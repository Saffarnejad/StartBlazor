using StartBlazor.Data;

namespace StartBlazor.Repositories.Contracts
{
    public interface IOrderRepository
    {
        public Task<Order> CreateAsync(Order order);
        public Task<IEnumerable<Order>> GetAllAsync(string? userId = null);
        public Task<Order> GetAsync(int id);
        public Task<Order> UpdateStatusAsync(int orderId, OrderStatus orderStatus);
    }
}
