using System.ComponentModel.DataAnnotations;

namespace PharmacyManagement.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MinLength(4)]
        public string? Description { get; set; }

        public IList<CategorieProduct>? Products { get; set; } = [];

        public IList<Rayon>? Rayons { get; set; } = [];
    }
}
