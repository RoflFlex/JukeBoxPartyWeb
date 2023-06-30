using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JukeBoxPartyAPI.Data;
using JukeBoxPartyAPI.Models;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JukeBoxPartyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueElementsController : ControllerBase
    {
        private readonly IQueueElementsRepository _queueElementRepository;
    
        public QueueElementsController(IQueueElementsRepository queueElementRepository)
        {
            _queueElementRepository = queueElementRepository;
        }
        /* public QueueElementsController()
     {
         _queueElementRepository = new QueueElementRepository(new MyDbContext());
     }*/

        // GET: api/QueueElements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueElement>>> GetQueueElements()
        {
            List<QueueElement> queueElements;
            try
            {
                queueElements = await _queueElementRepository.GetAllElements();
            }
            catch
            {
                return NotFound();
            }
            if(queueElements == null)
            {
                return NotFound();
            }
            return queueElements;
        }

        // GET: api/QueueElements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueElement>> GetQueueElement(int id)
        {
            try
            {
                var element =  await _queueElementRepository.GetElementById(id);
                if(element == null)
                {
                    return NotFound();
                }
                else
                {
                    return element;
                }
                
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Lobby/{id}")]
        public async Task<ActionResult<List<QueueElement>>> GetOpenSongs(Guid id)
        {
            try
            {
                return await _queueElementRepository.GetOpenElementsOfLobby(id);
            }
            catch
            {
                return NotFound();
            }
        }

        //// PUT: api/QueueElements/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutQueueElement(int id, QueueElement queueElement)
        //{
        //    if (id != queueElement.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(queueElement).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QueueElementExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("Play/{id}")]
        public async Task<IActionResult> Play(int id)
        {

            try
            {
                if (await _queueElementRepository.PlayQueueElement(id))
                return Ok();
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/QueueElements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueElement>> PostQueueElement(PostQueueElement queueElement)
        {
            try
            {
                var element = await _queueElementRepository.CreateQueueElement(queueElement);
                return CreatedAtAction("GetQueueElement", new { id = element.Id }, element);
            }
            catch(TimeoutException e)
            {
                if (e.Data.Contains("ProblemDetails"))
                {
                    var problemDetails = e.Data["ProblemDetails"] as ProblemDetails;
                    return BadRequest(problemDetails);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }           
        }




        /*// DELETE: api/QueueElements/5
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
        }*/
        /*[NonAction]
        private bool QueueElementExists(int id)
        {
            return (_context.QueueElements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [NonAction]
        public double GetDifferenceInSeconds(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date2 - date1;
            double seconds = difference.TotalSeconds;
            return seconds;
        }*/

    }
}
