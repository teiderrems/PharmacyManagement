using Microsoft.EntityFrameworkCore;

namespace PharmacyManagement.Models
{

    [PrimaryKey(nameof(OrderId),nameof(ProductId))]
    public class OrderProduct
    {

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
