namespace alacnz.server.Models
{
    public class SocialWorkTeam
    {
        public int SocialWorkTeamId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Specialty { get; set; }  // 'Wellbeing Worker', 'Social Worker', etc.
        public List<Case> Cases { get; set; } // Relationship with assigned cases
    }
}
