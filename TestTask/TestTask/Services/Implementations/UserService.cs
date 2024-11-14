using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUser()
        {
            var data = await _context.Users
                            .AsNoTracking()
                            .Where(p => p.Orders.Any(p => p.CreatedAt.Year == 2003))
                            .Select(p=> new { User = p, OrdersSummary=p.Orders.Count })
                            .OrderByDescending(p=> p.OrdersSummary)
                            .FirstOrDefaultAsync();

            var user =data?.User;

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var data = await _context.Users
                            .AsNoTracking()
                            .Where(p=> p.Orders.Any(p => p.CreatedAt.Year == 2010 && p.Status==Enums.OrderStatus.Paid))
                            .ToListAsync();

            return data;
        }
    }
}
