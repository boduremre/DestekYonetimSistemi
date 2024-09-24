namespace DestekYonetimSistemi.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SupportTicketId { get; set; }
        public SupportTicket SupportTicket { get; set; } // Navigation property
    }
}