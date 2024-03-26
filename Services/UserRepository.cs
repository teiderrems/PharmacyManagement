using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;
using System.Collections.Generic;

namespace PharmacyManagement.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IList<ApplicationUser>> FindAsync()
        {
            return await _context.Users.ToListAsync();
        }
        
        public async Task<ApplicationUser> FindById(string id)
        {
            return await _context.Users.FirstAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> FindByUserName(string username)
        {
            return await _context.Users.FirstAsync(x => x.UserName == username);
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            var hash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hash.HashPassword(user, user.PasswordHash!);
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User add successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
        {
            var hash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hash.HashPassword(user, user.PasswordHash!);
            _context.Users.Attach(user).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User update successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return user;
        }

        public async void DeleteUser(ApplicationUser user)
        {
            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User delete successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
