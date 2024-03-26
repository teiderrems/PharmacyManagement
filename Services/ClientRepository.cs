using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Services
{
    public class ClientRepository : IStoreManager<Client>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(ApplicationDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Client> CreateAsync(Client user)
        {
            _context.Clients.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Client add succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }

        public async void DeleteAsync(Client user)
        {
            _context.Clients.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Client deleted succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IList<Client>> FindAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> FindByIdAsync(int id)
        {
            return await _context.Clients.FirstAsync(s=>s.Id.Equals(id));
        }

        public async Task<Client> FindByNameAsync(string name)
        {
            return await _context.Clients.FirstAsync(s => s.Name!.Equals(name));
        }

        public async Task<Client> UpdateAsync(Client user)
        {
            _context.Clients.Attach(user).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Client update succesful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }
    }
}
