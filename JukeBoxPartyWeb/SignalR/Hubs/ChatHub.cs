using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using JukeBoxPartyWeb.Controllers;

namespace JukeBoxPartyWeb.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public System.Timers.Timer timer ;


        public async Task SwitchTrack(string roomname, string id)
        {
            int intid = int.Parse(id);
            await APICaller.MakeTrackPlayed(intid);
            await Clients.Group(roomname).SendAsync("OnSwitchTrack", id);
        }
        public async Task ChangeTrack(string roomname, string id)
        {
            int intid = int.Parse(id);
            await APICaller.MakeTrackPlayed(intid);
            await Clients.Group(roomname).SendAsync("OnSwitchTrack", id);
        }

        public async Task JoinRoom(string roomName)
        {
            /*if (timer == null)
            {
                timer= new Timer(1000000);
                timer.Elapsed += OnTimedEvent;
                timer.Start();

            }*/
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
        public async Task SendTrack(string roomname, string json)
        {
            /*if(!timer.Enabled)
            {
                timer = new System.Timers.Timer(Convert.ToDouble(name.Length*1000));
                timer.Elapsed += OnTimedEvent;
                //timer.AutoReset = true;
                timer.Start();
            }
*/
            var data = (JObject)JsonConvert.DeserializeObject(json);
            var songid = data.SelectToken("id").Value<int>();
            await APICaller.AddTrackToQueue(Guid.Parse(roomname), songid);
            Debug.WriteLine($"sended{json}");
            await Clients.Group(roomname).SendAsync("ReceiveTrack", json);
        }

        public async Task OnTrackEnded()
        {
            await Clients.All.SendAsync("TrackEnded", "sdsdsd");
        }

        

        public override Task OnConnectedAsync()
        {
           

            return base.OnConnectedAsync();
        }

    }
}
