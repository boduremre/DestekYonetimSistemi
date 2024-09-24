using DestekYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DestekYonetimSistemi
{


    public class SupportManagementDbContext : IdentityDbContext<ApplicationUser> // ApplicationUser: Asp.Net Identity kullanıcı modeli
    {
        public SupportManagementDbContext(DbContextOptions<SupportManagementDbContext> options)
            : base(options)
        {
        }

        // Destek Talebi Modeli DbSet'i
        public DbSet<SupportTicket> SupportTickets { get; set; }

        // Talep Yorumları Modeli DbSet'i
        public DbSet<TicketComment> TicketComments { get; set; }

        // Bildirimler Modeli DbSet'i (Opsiyonel)
        public DbSet<Notification> Notifications { get; set; }

        // DbContext konfigürasyonu (isteğe bağlı)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Destek Talebi durumu için Enum yapılandırması (isteğe bağlı)
            modelBuilder.Entity<SupportTicket>()
                .Property(t => t.Status)
                .HasConversion<string>(); // Enum ise, string olarak sakla

            // Talep önceliği için Enum yapılandırması (isteğe bağlı)
            modelBuilder.Entity<SupportTicket>()
                .Property(t => t.Priority)
                .HasConversion<string>(); // Enum ise, string olarak sakla
        }
    }

}
