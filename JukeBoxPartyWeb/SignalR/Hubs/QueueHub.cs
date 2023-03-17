using Microsoft.AspNetCore.SignalR;

namespace JukeBoxPartyWeb.SignalR.Hubs
{
    public class QueueHub:Hub
    {
        public async Task SendTrack(string name, string url)
        {
            await Clients.All.SendAsync("ReceiveTrack", name, url);
        }
    }
}
