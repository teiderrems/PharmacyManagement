using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagement.Models
{
    public class Client
    {
        public int Id {  get; set; }

        [StringLength(150,MinimumLength =4)]
        public string? Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, MinimumLength = 10)]
        public string? Address { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Birth Day")]
        public DateTime BirthDay { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Last Modified")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
    }
}
