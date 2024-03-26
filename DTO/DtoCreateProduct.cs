using PharmacyManagement.Models;

namespace PharmacyManagement.DTO
{
    public class DtoCreateProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public List<int> categories { get; set; }
    }
}
