using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class ProductRepository : IStoreManager<Product>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return product;
        }

        public async void DeleteAsync(Product product)
        {
            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Product>> FindAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            return await _context.Products.FirstAsync(s => s.Name!.Equals(name));
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Attach(product).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return product;
        }
    }
}
