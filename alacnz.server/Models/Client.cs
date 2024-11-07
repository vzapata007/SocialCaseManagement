namespace alacnz.server.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string ImmigrationStatus { get; set; }  // Citizen, Resident Visa, etc.
        public string SocialSecurityBenefit { get; set; }  // 'Yes', 'No'
        public string BeneficiariesNationality { get; set; }  // 'Latin American', 'NZ', etc.

        // Navigation Property to Cases
        public ICollection<Case> Cases { get; set; }  // One-to-many relationship with Case
    }
}
