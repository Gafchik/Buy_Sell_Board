using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buy_Sell_Board.Data;
using Buy_Sell_Board.Models.Announcement;

namespace Buy_Sell_Board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AnnouncementsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            return await _db.Announcements.ToListAsync();
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            var announcement = await _db.Announcements.FindAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            return announcement;
        }

        // PUT: api/Announcements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(int id, Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }

            _db.Entry(announcement).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
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

        // POST: api/Announcements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement announcement)
        {
            _db.Announcements.Add(announcement);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _db.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _db.Announcements.Remove(announcement);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementExists(int id)
        {
            return _db.Announcements.Any(e => e.Id == id);
        }
    }
}
