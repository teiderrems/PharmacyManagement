using System.ComponentModel.DataAnnotations;

namespace PharmacyManagement.Models
{
    
    public class Order
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public required Supplier Supplier { get; set; }

        public DateTime OrderDate { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.PENDING;
        public enum StatusEnum
        {
            PENDING,
            DELIVER
        }

        public IList<OrderProduct>? Products { get; set; } = [];

        public DateTime? DelivederDate { get; set; }
    }
}
