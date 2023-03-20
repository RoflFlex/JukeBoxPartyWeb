using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JukeBoxPartyAPI.Data;
using JukeBoxPartyAPI.Models;

namespace JukeBoxPartyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueElementsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public QueueElementsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/QueueElements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueElement>>> GetQueueElements()
        {
          if (_context.QueueElements == null)
          {
              return NotFound();
          }
            return await _context.QueueElements.ToListAsync();
        }

        // GET: api/QueueElements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueElement>> GetQueueElement(int id)
        {
          if (_context.QueueElements == null)
          {
              return NotFound();
          }
            var queueElement = await _context.QueueElements.FindAsync(id);

            if (queueElement == null)
            {
                return NotFound();
            }

            return queueElement;
        }

        // PUT: api/QueueElements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueElement(int id, QueueElement queueElement)
        {
            if (id != queueElement.Id)
            {
                return BadRequest();
            }

            _context.Entry(queueElement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueElementExists(id))
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

        // POST: api/QueueElements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueElement>> PostQueueElement(QueueElement queueElement)
        {
          if (_context.QueueElements == null)
          {
              return Problem("Entity set 'MyDbContext.QueueElements'  is null.");
          }
            _context.QueueElements.Add(queueElement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueueElement", new { id = queueElement.Id }, queueElement);
        }

        // DELETE: api/QueueElements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueueElement(int id)
        {
            if (_context.QueueElements == null)
            {
                return NotFound();
            }
            var queueElement = await _context.QueueElements.FindAsync(id);
            if (queueElement == null)
            {
                return NotFound();
            }

            _context.QueueElements.Remove(queueElement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QueueElementExists(int id)
        {
            return (_context.QueueElements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
