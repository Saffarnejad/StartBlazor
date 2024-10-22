using StartBlazor.Data;

namespace StartBlazor.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        public Task<bool> ClearAsync(string? userId);
        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);
        public Task<bool> UpdateAsync(string userId, int productId, int count);
    }
}
