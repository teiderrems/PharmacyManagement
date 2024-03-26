using Microsoft.EntityFrameworkCore;

namespace PharmacyManagement.Models
{

    [PrimaryKey(nameof(VenteId),nameof(ProductId))]
    public class VenteProduct
    {
        public int VenteId { get; set; }
        public int ProductId { get; set; }

        public int Quantity {  get; set; }
        public Vente? Vente { get; set; }

        public Product? Product { get; set; }
    }
}
