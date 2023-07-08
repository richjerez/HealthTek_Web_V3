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
    [Route("api/authorizations")]
    [ApiController]
    public class AuthorizationsApiController : ControllerBase
    {
        private readonly IdentityContext _context;

        public AuthorizationsApiController(IdentityContext context)
        {
            _context = context;
        }

        // GET: api/AuthorizationsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authorizations>>> GetAuthorizations()
        {
            return await _context.Authorizations.ToListAsync();
        }

        // GET: api/AuthorizationsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authorizations>> GetAuthorizations(int id)
        {
            var authorizations = await _context.Authorizations.FindAsync(id);

            if (authorizations == null)
            {
                return NotFound();
            }

            return authorizations;
        }

        // PUT: api/AuthorizationsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorizations(int id, Authorizations authorizations)
        {
            if (id != authorizations.AuthorizationsId)
            {
                return BadRequest();
            }

            _context.Entry(authorizations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationsExists(id))
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

        // POST: api/AuthorizationsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Authorizations>> PostAuthorizations(Authorizations authorizations)
        {
            _context.Authorizations.Add(authorizations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorizations", new { id = authorizations.AuthorizationsId }, authorizations);
        }

        // DELETE: api/AuthorizationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorizations(int id)
        {
            var authorizations = await _context.Authorizations.FindAsync(id);
            if (authorizations == null)
            {
                return NotFound();
            }

            _context.Authorizations.Remove(authorizations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorizationsExists(int id)
        {
            return _context.Authorizations.Any(e => e.AuthorizationsId == id);
        }
    }
}
