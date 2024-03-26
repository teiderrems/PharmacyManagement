using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class OrderRepository : IStoreManager<Order>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return order;
        }

        public async void DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Order>> FindAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await _context.Orders.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Order> FindByNameAsync(string name)
        {
            return await _context.Orders.FirstAsync(s => s.Name!.Equals(name));
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Attach(order).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return order;
        }
    }
}
