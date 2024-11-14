using TestTask.Services.Interfaces;
using TestTask.Data;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrder()
        {
            var data = await _context.Orders
                         .AsNoTracking()
                         .Where(p=> p.Quantity>1)
                         .OrderByDescending(p => p.CreatedAt)
                         .FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Order>> GetOrders()
        {
            var data = await _context.Orders
                            .AsNoTracking()
                            .Where(p => p.User.Status == Enums.UserStatus.Active)
                            .OrderBy(p => p.CreatedAt)
                            .ToListAsync();

            return data;
        }
    }
}
