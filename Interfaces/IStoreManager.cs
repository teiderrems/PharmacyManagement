using PharmacyManagement.DTO;
using PharmacyManagement.Models;

namespace PharmacyManagement.Interfaces
{
    public interface IStoreManager<T> where T : class
    {
        public Task<IList<T>> FindAsync(DtoPagination? pagination);
        public Task<T> FindByIdAsync(int id);

        public Task<T> FindByNameAsync(string name);

        public Task<T> CreateAsync(T value);

        public Task<T> UpdateAsync(T value);

        public void DeleteAsync(T value);
    }
}
