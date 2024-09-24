namespace DestekYonetimSistemi.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
