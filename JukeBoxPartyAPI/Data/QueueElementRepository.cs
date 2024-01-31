using JukeBoxPartyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxPartyAPI.Data
{
    public class QueueElementRepository : IQueueElementsRepository
    {
        private readonly MyDbContext _context;
        private readonly double _interval = 90.0;
        public QueueElementRepository(MyDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<QueueElement> CreateQueueElement(PostQueueElement queueElement)
        {
            if (_context.QueueElements == null)
            {
                throw new InvalidOperationException("QueueElements collection is null."); ;
            }
            Song? song = await _context.Songs.FindAsync(queueElement.SongId);
            Lobby? lobby = await _context.Lobbies.FindAsync(queueElement.LobbyId);
            if (song == null || lobby == null)
            {
                //song = await _context.Songs.LastAsync();
                throw new NullReferenceException("Lobby or song is not found");
            }


            var lastAddedQueueElement = _context.QueueElements.Where(q => q.UserId == queueElement.UserId).OrderBy(q => q.AddedAt).ToList().LastOrDefault();

            if (lastAddedQueueElement == null || GetDifferenceInSeconds(lastAddedQueueElement.AddedAt, DateTime.Now) >= _interval)
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

                return element;
            }
            else
            {
                double difference = _interval - GetDifferenceInSeconds(lastAddedQueueElement.AddedAt, DateTime.Now);
                var problemDetails = new ProblemDetails
                {
                    Status = 400,
                    Title = "Invalid request",
                    Detail = difference.ToString()
                };
                throw new TimeoutException("The data is invalid.") { Data = { ["ProblemDetails"] = problemDetails } };
            }

        }

        public async Task<List<QueueElement>> GetAllElements()
        {
            if (_context.QueueElements == null)
            {
                return new List<QueueElement>();
            }
            return await _context.QueueElements.ToListAsync();
        }

        public async Task<QueueElement> GetElementById(int id)
        {
            if (_context.QueueElements == null)
            {
                throw new InvalidOperationException("QueueElements collection is null.");
            }
            var queueElement = await _context.QueueElements.FindAsync(id);

            return queueElement;
        }

        public async Task<List<QueueElement>> GetOpenElementsOfLobby(Guid lobbyID)
        {
            if (_context.QueueElements == null)
            {
                throw new InvalidOperationException("QueueElements collection is null.");
            }
            var queueElementList = await _context.QueueElements.ToListAsync();
            
            var queueElements = queueElementList.FindAll(element => element.LobbyId == lobbyID && element.PlayedAt == null);
            if (queueElementList.FindLast(element => element.LobbyId == lobbyID && element.PlayedAt.HasValue) != null)
            {
                var queueElWithPlayedAt = queueElementList.FindLast(element => element.LobbyId == lobbyID && element.PlayedAt.HasValue);

                TimeSpan span = (TimeSpan)(DateTime.Now - queueElWithPlayedAt.PlayedAt);

                double ms = (double)span.TotalMilliseconds;
                if (ms < queueElWithPlayedAt.Song.Duration)

                    queueElements.Insert(0, queueElementList.FindLast(element => element.LobbyId == lobbyID && element.PlayedAt.HasValue));
            }

            return queueElements;
        }

        public async Task<bool> PlayQueueElement(int id)
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
                    return false;
                }
                return false;
            }

            return true;
        }
        private bool QueueElementExists(int id)
        {
            return (_context.QueueElements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private double GetDifferenceInSeconds(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date2 - date1;
            double seconds = difference.TotalSeconds;
            return seconds;
        }
    }
}
