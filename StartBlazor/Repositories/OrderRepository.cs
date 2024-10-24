using Microsoft.EntityFrameworkCore;
using StartBlazor.Data;
using StartBlazor.Repositories.Contracts;

namespace StartBlazor.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.Date = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string? userId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return await _context.Orders.ToListAsync();
            }
            return await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _context.Orders
                .Include(order => order.OrderItems)
                .SingleOrDefaultAsync(order => order.Id == id);
        }

        public async Task<Order> UpdateStatusAsync(int orderId, OrderStatus orderStatus)
        {
            var orderIdDb = await _context.Orders.SingleOrDefaultAsync(order => order.Id == orderId);
            if (orderIdDb is not null)
            {
                orderIdDb.Status = orderStatus.ToString();
                await _context.SaveChangesAsync();
            }
            return orderIdDb;
        }
    }
}
