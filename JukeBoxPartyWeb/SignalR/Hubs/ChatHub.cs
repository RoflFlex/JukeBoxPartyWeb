using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Timers;

namespace JukeBoxPartyWeb.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public System.Timers.Timer timer = new System.Timers.Timer(10000);
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("JoinedRoom",Context.User.Identity.Name + " joined.");
        }
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("LeftRoom", Context.User.Identity.Name + " left.");
        }
        public async Task SendMessage(string roomName, string user, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendTrack(string name, string url)
        {
            /*if(!timer.Enabled)
            {
                timer = new System.Timers.Timer(Convert.ToDouble(name.Length*1000));
                timer.Elapsed += OnTimedEvent;
                //timer.AutoReset = true;
                timer.Start();
            }
*/
            Debug.WriteLine($"sended{name}");
            await Clients.All.SendAsync("ReceiveTrack", name, url);
        }
        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            timer.Dispose();
            // await Clients.All.SendAsync("TrackEnded");
        }
        public async Task OnTrackEnded()
        {
            await Clients.All.SendAsync("TrackEnded", "sdsdsd");
        }

        public async Task IsNextTrack()
        {
            if (timer.Enabled)
            {
                await Clients.All.SendAsync("IsNextTrack", "false");
            }
            else
            {
                await Clients.All.SendAsync("IsNextTrack", "true");

            }
        }

        public override Task OnConnectedAsync()
        {
            if (!timer.Enabled)
            {
                timer.Elapsed += OnTimedEvent;
                timer.Start();
                Console.WriteLine("sdfsdfsdsds");
            }

            return base.OnConnectedAsync();
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
