using DestekYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestekYonetimSistemi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTicketsController : ControllerBase
    {
        private readonly SupportManagementDbContext _context;

        public SupportTicketsController(SupportManagementDbContext context)
        {
            _context = context;
        }

        // 1. Destek talebi oluşturma
        [HttpPost]
        [Authorize(Roles = "Admin")] // Sadece admin rolündekiler erişebilir
        public async Task<ActionResult<SupportTicket>> CreateTicket(SupportTicket ticket)
        {
            _context.SupportTickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }

        // 2. Destek taleplerini listeleme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportTicket>>> GetTickets()
        {
            return await _context.SupportTickets.ToListAsync();
        }

        // 3. Tek bir destek talebini getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<SupportTicket>> GetTicketById(int id)
        {
            var ticket = await _context.SupportTickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // 4. Destek talebini güncelleme
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Sadece admin rolündekiler erişebilir
        public async Task<IActionResult> UpdateTicket(int id, SupportTicket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 5. Destek talebini silme
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Sadece admin rolündekiler erişebilir
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.SupportTickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.SupportTickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.SupportTickets.Any(e => e.Id == id);
        }
    }
}
