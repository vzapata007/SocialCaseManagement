namespace alacnz.server.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceType { get; set; }  // 'Advocacy', 'Mental Health', etc.
        public string Description { get; set; }
        public List<Case> Cases { get; set; }  // Relationship with cases that receive the service
    }
}
