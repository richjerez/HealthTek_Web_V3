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
    [Route("api/documentationprocesses")]
    [ApiController]
    public class DocumentationProcessesApiController : ControllerBase
    {
        private readonly IdentityContext _context;

        public DocumentationProcessesApiController(IdentityContext context)
        {
            _context = context;
        }

        // GET: api/DocumentationProcessesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentationProcess>>> GetDocumentationProcess()
        {
            return await _context.DocumentationProcess.ToListAsync();
        }

        // GET: api/DocumentationProcessesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentationProcess>> GetDocumentationProcess(int id)
        {
            var documentationProcess = await _context.DocumentationProcess.FindAsync(id);

            if (documentationProcess == null)
            {
                return NotFound();
            }

            return documentationProcess;
        }

        // PUT: api/DocumentationProcessesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentationProcess(int id, DocumentationProcess documentationProcess)
        {
            if (id != documentationProcess.DocumentationProcessId)
            {
                return BadRequest();
            }

            _context.Entry(documentationProcess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentationProcessExists(id))
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

        // POST: api/DocumentationProcessesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentationProcess>> PostDocumentationProcess(DocumentationProcess documentationProcess)
        {
            _context.DocumentationProcess.Add(documentationProcess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentationProcess", new { id = documentationProcess.DocumentationProcessId }, documentationProcess);
        }

        // DELETE: api/DocumentationProcessesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentationProcess(int id)
        {
            var documentationProcess = await _context.DocumentationProcess.FindAsync(id);
            if (documentationProcess == null)
            {
                return NotFound();
            }

            _context.DocumentationProcess.Remove(documentationProcess);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentationProcessExists(int id)
        {
            return _context.DocumentationProcess.Any(e => e.DocumentationProcessId == id);
        }
    }
}
