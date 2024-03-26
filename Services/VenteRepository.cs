using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class VenteRepository : IStoreManager<Vente>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<VenteRepository> _logger;

        public VenteRepository(ApplicationDbContext context, ILogger<VenteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Vente> CreateAsync(Vente vente)
        {
            _context.Ventes.Add(vente);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Vente add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return vente;
        }

        public async void DeleteAsync(Vente vente)
        {
            _context.Ventes.Remove(vente);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Vente deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Vente>> FindAsync()
        {
            return await _context.Ventes.ToListAsync();
        }

        public async Task<Vente> FindByIdAsync(int id)
        {
            return await _context.Ventes.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Vente> FindByNameAsync(string name)
        {
            return await _context.Ventes.FirstAsync(s => s.Title== name);
        }

        public async Task<Vente> UpdateAsync(Vente vente)
        {
            _context.Ventes.Attach(vente).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Vente update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return vente;
        }
    }
}
