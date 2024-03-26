using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class RayonRepository : IStoreManager<Rayon>
    {

        private readonly ApplicationDbContext _context;

        private readonly ILogger<RayonRepository> _logger;

        public RayonRepository(ApplicationDbContext context, ILogger<RayonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Rayon> CreateAsync(Rayon rayon)
        {
            _context.Rayons.Add(rayon);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{nameof(rayon)} add successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return rayon;
        }

        public async void DeleteAsync(Rayon rayon)
        {
            _context.Rayons.Remove(rayon);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{nameof(rayon)} add successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Rayon>> FindAsync()
        {
            return await _context.Rayons.ToListAsync();
        }

        public async Task<Rayon> FindByIdAsync(int id)
        {
            return await _context.Rayons.FirstAsync(x => x.Id == id);
        }

        public async Task<Rayon> FindByNameAsync(string name)
        {
            return await _context.Rayons.FirstAsync(r=>r.Name == name);
        }

        public async Task<Rayon> UpdateAsync(Rayon rayon)
        {
            _context.Rayons.Attach(rayon).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{nameof(rayon)} update successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return rayon;
        }
    }
}
