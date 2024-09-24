namespace DestekYonetimSistemi.Models
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SupportTicketStatus Status { get; set; } // Enum olarak durumu belirt
        public SupportTicketPriority Priority { get; set; } // Enum olarak önceliği belirt
        public string UserId { get; set; } // foreign key to AspNet Identity User
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public enum SupportTicketStatus
    {
        Open,      // Açık
        Answered,  // Yanıtlandı
        Resolved,   // Çözüldü
        Closed     // Kapalı
    }

    public enum SupportTicketPriority
    {
        Low,      // Düşük
        Medium,   // Orta
        High      // Yüksek
    }
}
