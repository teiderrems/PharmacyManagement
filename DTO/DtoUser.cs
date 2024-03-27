namespace PharmacyManagement.DTO
{
    public class DtoUser
    {
        public string? UserName { get; set; }
        public required string Email { get; set; }

        public DateTime BirthDay { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
