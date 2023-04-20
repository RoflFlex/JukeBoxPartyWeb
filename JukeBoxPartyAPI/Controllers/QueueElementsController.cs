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

namespace JukeBoxPartyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueElementsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly double _interval = 90.0;
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

        [HttpGet("Lobby/{id}")]
        public async Task<ActionResult<List<QueueElement>>> GetOpenSongs(Guid id)
        {
            if (_context.QueueElements == null)
            {
                return NotFound();
            }
            var queueElementList = await _context.QueueElements.ToListAsync();

            var queueElements = queueElementList.FindAll(element => element.LobbyId == id && element.PlayedAt == null);
            if(queueElementList.FindLast(element => element.LobbyId == id && element.PlayedAt.HasValue) != null){
                var queueElWithPlayedAt = queueElementList.FindLast(element => element.LobbyId == id && element.PlayedAt.HasValue);

                TimeSpan span = (TimeSpan)(DateTime.Now - queueElWithPlayedAt.PlayedAt);

                double ms = (double)span.TotalMilliseconds;
                if (ms < queueElWithPlayedAt.Song.Duration)

                queueElements.Insert(0, queueElementList.FindLast(element => element.LobbyId == id && element.PlayedAt.HasValue));
            }
            if (queueElements.Count == 0)
            {
                return NotFound();
            }

            return queueElements;
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

        [HttpPut("Play/{id}")]
        public async Task<IActionResult> Play(int id)
        {
           
            var queueElement = await _context.QueueElements.FindAsync(id);
            queueElement.PlayedAt = DateTime.Now;
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
        public async Task<ActionResult<QueueElement>> PostQueueElement(PostQueueElement queueElement)
        {
            if (_context.QueueElements == null)
            {
                return Problem("Entity set 'MyDbContext.QueueElements'  is null.");
            }
            Song? song = await _context.Songs.FindAsync(queueElement.SongId);
            Lobby? lobby = await _context.Lobbies.FindAsync(queueElement.LobbyId);
            if (song == null || lobby == null)
            {
                //song = await _context.Songs.LastAsync();
                return BadRequest();
            }
            

                var lastAddedQueueElement = _context.QueueElements.Where(q => q.UserId == queueElement.UserId).OrderBy(q => q.AddedAt).ToList().LastOrDefault();

            if (lastAddedQueueElement == null || GetDifferenceInSeconds(lastAddedQueueElement.AddedAt,DateTime.Now) >= _interval) 
                {
                    QueueElement element = new QueueElement()
                    {
                        AddedAt = DateTime.Now,
                        Lobby = lobby,
                        Song = song,
                        UserId = queueElement.UserId,
                        LobbyId = queueElement.LobbyId,
                        SongId = queueElement.SongId
                    };
                    _context.QueueElements.Add(element);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetQueueElement", new { id = element.Id }, element);
                }
                else
                {
                /* var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                 var data = new { Seconds = GetDifferenceInSeconds(lastAddedQueueElement.AddedAt,DateTime.Now)};
                 var json = JsonConvert.SerializeObject(data);
                 response.Content = new StringContent(json, Encoding.UTF8, "application/json");*/
                double difference = _interval - GetDifferenceInSeconds(lastAddedQueueElement.AddedAt, DateTime.Now);
                    var problemDetails = new ProblemDetails
                    {
                        Status = 400,
                        Title = "Invalid request",
                        Detail = difference.ToString()
                    };
                    return BadRequest(problemDetails);
                }
            
          
           
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
        [NonAction]
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
        }

    }
}
