namespace PharmacyManagement.Models
{
    public class Rayon
    {
        public int Id { get; set; }

        public Categorie? Categorie { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
