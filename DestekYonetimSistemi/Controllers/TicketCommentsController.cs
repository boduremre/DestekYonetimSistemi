using DestekYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DestekYonetimSistemi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketCommentsController : ControllerBase
    {
        private readonly SupportManagementDbContext _context;

        public TicketCommentsController(SupportManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult<IEnumerable<TicketComment>>> GetComments(int ticketId)
        {
            return await _context.TicketComments
                .Where(c => c.SupportTicketId == ticketId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TicketComment>> CreateComment(TicketComment comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            _context.TicketComments.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetComments), new { ticketId = comment.SupportTicketId }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, TicketComment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.TicketComments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.TicketComments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
