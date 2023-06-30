using JukeBoxPartyAPI.Models;

namespace JukeBoxPartyAPI.Data
{
    public interface IQueueElementsRepository
    {
        public Task<List<QueueElement>> GetAllElements();
        public Task<QueueElement> GetElementById(int id);
        public Task<List<QueueElement>> GetOpenElementsOfLobby(Guid lobbyID);
        public Task<bool> PlayQueueElement(int id);
        public Task<QueueElement> CreateQueueElement(PostQueueElement queueElement);

    }
}
