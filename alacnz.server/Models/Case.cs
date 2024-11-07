namespace alacnz.server.Models
{
    public class Case
    {
        public int CaseId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string Services { get; set; }
        public string ReferredBy { get; set; }
        public string ReferralReason { get; set; }
        public string ActivationFeasibility { get; set; }

        public int? ClientId { get; set; }  // FK for Client
        public Client Client { get; set; }  // Navigation property to Client

        public int? SocialWorkTeamId { get; set; }  // FK for SocialWorkTeam
        public SocialWorkTeam SocialWorkTeam { get; set; }  // Navigation property to SocialWorkTeam

        public int? ServiceId { get; set; }  // FK for Service
        public Service Service { get; set; }  // Navigation property to Service

        public List<Beneficiary> Beneficiaries { get; set; }
        public List<Session> Sessions { get; set; }  // Navigation property to Sessions
    }
}