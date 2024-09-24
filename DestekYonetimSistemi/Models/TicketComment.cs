namespace DestekYonetimSistemi.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public required string Comment { get; set; }
        public required string UserId { get; set; } // foreign key to AspNet Identity User
        public int TicketId { get; set; } // foreign key to SupportTicket
        public DateTime CreatedAt { get; set; }
    }

}
