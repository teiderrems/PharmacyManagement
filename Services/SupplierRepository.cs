using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class SupplierRepository : IStoreManager<Supplier>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<SupplierRepository> _logger;

        public SupplierRepository(ApplicationDbContext context, ILogger<SupplierRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Supplier> CreateAsync(Supplier user)
        {
            _context.Suppliers.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Supplier add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }

        public async void DeleteAsync(Supplier user)
        {
            _context.Suppliers.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Supplier deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Supplier>> FindAsync(DtoPagination? pagination)
        {
            if (pagination == null)
            {
                pagination = new()
                {
                    Limit = 20,
                    Page = 0
                };
            }
            int skip = pagination.Limit * pagination.Page;
            return await _context.Suppliers.Take(pagination.Limit).Skip(skip)
                .ToListAsync();
        }

        public async Task<Supplier> FindByIdAsync(int id)
        {
            return await _context.Suppliers.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Supplier> FindByNameAsync(string name)
        {
            return await _context.Suppliers.FirstAsync(s => s.Name!.Equals(name));
        }

        public async Task<Supplier> UpdateAsync(Supplier user)
        {
            _context.Suppliers.Attach(user).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Supplier update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }
    }
}
