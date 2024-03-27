namespace PharmacyManagement.DTO
{
    public class DtoCreateVente
    {
        public string Title {  get; set; }

        public int Owner {  get; set; }

        public List<int> products { get; set; }

        public decimal price { get; set; }
    }
}
