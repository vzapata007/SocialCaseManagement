namespace alacnz.server.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public string SessionType { get; set; }  // 'Counseling', 'Advocacy', etc.
        public string Outcome { get; set; }  // 'Completed', 'Pending'
        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
