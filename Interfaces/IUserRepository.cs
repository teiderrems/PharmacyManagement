using PharmacyManagement.DTO;
using PharmacyManagement.Models;

namespace PharmacyManagement.Interfaces
{
    public interface IUserRepository
    {
        public Task<IList<ApplicationUser>> FindAsync(DtoPagination? pagination);
        public Task<ApplicationUser> FindById(string id);

        public Task<ApplicationUser> FindByUserName(string username);

        public Task<ApplicationUser> CreateUser(ApplicationUser user);

        public Task<ApplicationUser> UpdateUser(ApplicationUser user);

        public void DeleteUser(ApplicationUser user);

    }
}
