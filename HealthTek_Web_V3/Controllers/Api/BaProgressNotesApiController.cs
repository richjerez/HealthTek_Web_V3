using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.Api
{
    [Authorize]
    [Route("api/baprogressnotes")]
    [ApiController]
    public class BaProgressNotesApiController : ControllerBase
    {
        private readonly IdentityContext _context;

        public BaProgressNotesApiController(IdentityContext context)
        {
            _context = context;
        }

        // GET: api/BaProgressNotesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaProgressNotes>>> GetBaProgressNotes()
        {
            return await _context.BaProgressNotes.ToListAsync();
        }

        // GET: api/BaProgressNotesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaProgressNotes>> GetBaProgressNotes(int id)
        {
            var baProgressNotes = await _context.BaProgressNotes.FindAsync(id);

            if (baProgressNotes == null)
            {
                return NotFound();
            }

            return baProgressNotes;
        }

        // PUT: api/BaProgressNotesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaProgressNotes(int id, BaProgressNotes baProgressNotes)
        {
            if (id != baProgressNotes.BaProgressNotesId)
            {
                return BadRequest();
            }

            _context.Entry(baProgressNotes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaProgressNotesExists(id))
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

        // POST: api/BaProgressNotesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaProgressNotes>> PostBaProgressNotes(BaProgressNotes baProgressNotes)
        {
            _context.BaProgressNotes.Add(baProgressNotes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaProgressNotes", new { id = baProgressNotes.BaProgressNotesId }, baProgressNotes);
        }

        // DELETE: api/BaProgressNotesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaProgressNotes(int id)
        {
            var baProgressNotes = await _context.BaProgressNotes.FindAsync(id);
            if (baProgressNotes == null)
            {
                return NotFound();
            }

            _context.BaProgressNotes.Remove(baProgressNotes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BaProgressNotesExists(int id)
        {
            return _context.BaProgressNotes.Any(e => e.BaProgressNotesId == id);
        }
    }
}
