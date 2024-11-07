namespace alacnz.server.Models
{
    public class Beneficiary
    {
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Category { get; set; }  // 'Adult', 'Child', 'Youth'
        public string Nationality { get; set; }

        // Foreign Key for Case
        public int CaseId { get; set; }
        public Case Case { get; set; }  // Navigation Property to Case
    }
}
