using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class CategorieRepository : IStoreManager<Categorie>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<CategorieRepository> _logger;

        public CategorieRepository(ApplicationDbContext context, ILogger<CategorieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Categorie> CreateAsync(Categorie cat)
        {
            _context.Categories.Add(cat);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Client add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return cat;
        }

        public async void DeleteAsync(Categorie cat)
        {
            _context.Categories.Remove(cat);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Categorie deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Categorie>> FindAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categorie> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Categorie> FindByNameAsync(string name)
        {
            return await _context.Categories.FirstAsync(s => s.Name!.Equals(name));
        }

        public async Task<Categorie> UpdateAsync(Categorie cat)
        {
            _context.Categories.Attach(cat).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("categorie update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return cat;
        }
    }
}
