using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagement.Models
{
    public class Vente
    {
        public int Id { get; set; }

        public string Title { get; set; }


        [DataType("decimal(10,2)")]
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Date of Day")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public required Client Client { get; set; }

        public IList<VenteProduct>? Products { get; set; } = [];
    }
}
