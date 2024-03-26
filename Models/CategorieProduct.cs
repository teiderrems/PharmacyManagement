using Microsoft.EntityFrameworkCore;

namespace PharmacyManagement.Models
{

    [PrimaryKey(nameof(ProductId),nameof(CategorieId))]
    public class CategorieProduct
    {

        public int ProductId { get; set; }

        public int CategorieId { get; set; }
        public Product? Product { get; set; }

        public Categorie? Categorie { get; set; }
    }
}
