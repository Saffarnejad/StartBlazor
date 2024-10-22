using Microsoft.EntityFrameworkCore;
using StartBlazor.Data;
using StartBlazor.Repositories.Contracts;

namespace StartBlazor.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClearAsync(string? userId)
        {
            var shoppingCarts = await _context.ShoppingCarts
                .Where(shoppingCart => shoppingCart.UserId == userId)
                .ToListAsync();
            _context.ShoppingCarts.RemoveRange(shoppingCarts);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _context.ShoppingCarts
                .Where(shoppingCart => shoppingCart.UserId == userId)
                .Include(shoppingCart => shoppingCart.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(string userId, int productId, int count)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(shoppingCart => shoppingCart.UserId == userId && shoppingCart.ProductId == productId);

            if (shoppingCart is null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = count
                };
                await _context.AddAsync(shoppingCart);
            }
            else
            {
                shoppingCart.Count += count;
                if (shoppingCart.Count <= 0)
                {
                    _context.Remove(shoppingCart);
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
