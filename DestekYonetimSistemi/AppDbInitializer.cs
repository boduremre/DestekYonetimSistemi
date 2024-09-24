using DestekYonetimSistemi.Models;
using DestekYonetimSistemi;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var context = serviceProvider.GetRequiredService<SupportManagementDbContext>();

        // Rol tanımları
        string adminRole = "Admin";
        string userRole = "User";

        // Rolleri oluştur
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        if (!await roleManager.RoleExistsAsync(userRole))
        {
            await roleManager.CreateAsync(new IdentityRole(userRole));
        }

        // Admin kullanıcısını oluştur
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!";

        var existingAdminUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdminUser == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true // E-posta onayı gereksiz
            };

            var adminResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (adminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }

        // User rolünde bir kullanıcı oluştur
        string userEmail = "user@example.com";
        string userPassword = "User123!";

        var existingUser = await userManager.FindByEmailAsync(userEmail);
        if (existingUser == null)
        {
            var regularUser = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true // E-posta onayı gereksiz
            };

            var userResult = await userManager.CreateAsync(regularUser, userPassword);
            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(regularUser, userRole);
            }
        }

        // Rastgele destek talepleri oluştur
        if (!context.SupportTickets.Any())
        {
            var supportTickets = new List<SupportTicket>
            {
                new SupportTicket
                {
                    Title = "Hesap Girişi Sorunu",
                    Description = "Hesabımda giriş yapamıyorum.",
                    Status = SupportTicketStatus.Open,
                    Priority = SupportTicketPriority.High,
                    UserId = "027e377b-1351-4920-9ef2-3f579034ae1c",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new SupportTicket
                {
                    Title = "Şifre Değiştirme",
                    Description = "Şifremi unuttum, sıfırlama talep ediyorum.",
                    Status = SupportTicketStatus.Open,
                    Priority = SupportTicketPriority.Medium,
                    UserId = "027e377b-1351-4920-9ef2-3f579034ae1c",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new SupportTicket
                {
                    Title = "Hizmet Hatası",
                    Description = "Kullandığım hizmette bir hata var.",
                    Status = SupportTicketStatus.Closed,
                    Priority = SupportTicketPriority.Low,
                    UserId = "027e377b-1351-4920-9ef2-3f579034ae1c",
                    CreatedAt = DateTime.UtcNow.AddDays(-3), // 3 gün önce oluşturulmuş
                    UpdatedAt = DateTime.UtcNow.AddDays(-1) // 1 gün önce güncellenmiş
                }
            };

            await context.SupportTickets.AddRangeAsync(supportTickets);
            await context.SaveChangesAsync();
        }
    }
}
