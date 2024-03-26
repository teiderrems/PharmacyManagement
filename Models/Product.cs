using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PharmacyManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MinLength(2)]
        public required string Name { get; set; }

        [MinLength(10)]
        public required string Description { get; set; }

        public int StockQuantity { get; set; } = 0;

        [DataType("decimal(5,2)")]
        public decimal Price { get; set; } = decimal.Zero;


        [DataType(DataType.Date)]
        [DisplayName("Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Last Modified")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        public IList<VenteProduct>? Ventes { get; set; } = [];
        public IList<CategorieProduct>? Categories { get; set; } = [];

        public IList<OrderProduct>? Orders { get; set; } = [];
    }
}
