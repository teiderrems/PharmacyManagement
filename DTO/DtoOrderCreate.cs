namespace PharmacyManagement.DTO
{
    public class DtoOrderCreate
    {
        public List<int> products {  get; set; }

        public string name { get; set; }

        public int supplier {  get; set; }
    }
}
