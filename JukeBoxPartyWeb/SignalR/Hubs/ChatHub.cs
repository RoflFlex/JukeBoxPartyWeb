using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace JukeBoxPartyWeb.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public Timer timer;
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendTrack(string name, string url)
        {

            Debug.WriteLine($"sended{name}");
            await Clients.All.SendAsync("ReceiveTrack", name, url);
        }
        /*  public override async Task OnConnected()
          {
              _userCount++;
              await Clients.All.online(_userCount);
          }
          public override Task OnReconnected()
          {
              _userCount++;
              var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
              context.Clients.All.online(_userCount);
          }
          public override Task OnDisconnected(bool stopCalled)
          {
              _userCount--;
              var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
              context.Clients.All.online(_userCount);
          }*/
    }
}
