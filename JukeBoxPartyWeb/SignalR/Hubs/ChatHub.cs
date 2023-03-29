using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using JukeBoxPartyWeb.Controllers;
using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using JukeBoxPartyWeb.Data;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace JukeBoxPartyWeb.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private ApplicationDbContext dbContext;
        private static Dictionary<string, List<string>> lobbies = new Dictionary<string, List<string>>();

        public ChatHub(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
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
            if (!lobbies.ContainsKey(roomName))
            {
                lobbies.Add(roomName, new List<string>());
            }
            lobbies.TryGetValue(roomName, out var list);
            

            var email = Context.User.Identity.Name;

            list.Add(email);
            
            // Query for all blogs with names starting with B
            var nickname = (from b in dbContext.Users
                    where b.UserName == email
                    select b.NickName).FirstOrDefault();

            // Query for the Blog named ADO.NET Blog
            var usercards = await GetAccounts(roomName);
            string bodystring = JsonConvert.SerializeObject(usercards);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("JoinedRoom",nickname  + " joined.", bodystring);
        }
        public async Task LeaveRoom(string roomName)
        {
            var email = Context.User.Identity.Name;
            lobbies.TryGetValue(roomName, out var list);
            list.Remove(email);

            // Query for all blogs with names starting with B
            var nickname = (from b in dbContext.Users
                            where b.UserName == email
                            select b.NickName).FirstOrDefault();

            // Query for the Blog named ADO.NET Blog
            var usercards = await GetAccounts(roomName);
            string bodystring = JsonConvert.SerializeObject(usercards);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("LeftRoom", nickname + " left.",usercards);
        }
        public async Task SendMessage(string roomName,  string message)
        {
            var nickname = GetNickName(Context.User.Identity.Name);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", nickname, message);
        }

        public async Task SendTrack(string roomname, string json)
        {
             var data = (JObject)JsonConvert.DeserializeObject(json);
            var songid = data.SelectToken("id").Value<int>();
            await APICaller.AddTrackToQueue(Guid.Parse(roomname), songid);
            Debug.WriteLine($"sended{json}");
            await Clients.Group(roomname).SendAsync("ReceiveTrack", json);
        }
        private string GetNickName(string email)
        {
            var nickname = (from b in dbContext.Users
                            where b.UserName == email
                            select b.NickName).FirstOrDefault();
            return nickname;
        }
        private async Task<List<UserCard>> GetAccounts(string roomName)
        {
            var accounts = new List<UserCard>();
            var lobby = lobbies.GetValueOrDefault(roomName);

            for (int i =0; i <lobby.Count; i++)
            {
                Account account = await GetAccount(lobby[i]);
                accounts.Add(new UserCard()
                {
                    NickName = account.User.NickName,
                    Url = "/media/images/" + account.Role.ImageUrl,
                }) ;
            }


            return accounts;

        }


        private async Task<Account> GetAccount(string username)
        {
            ApplicationUser  user = GetApplicationUser(username);
            ApplicationRole role = await GetRoleOfUser(user);
            return new Account { User = user, Role = role };
        }
        private ApplicationUser GetApplicationUser(string username)
        {
            ApplicationUser user = (from b in dbContext.Users
                                    where b.UserName == username
                                    select b).FirstOrDefault();
            return user;
        }
        private async Task<ApplicationRole> GetRoleOfUser(ApplicationUser user)
        {

            ApplicationRole role = new ApplicationRole();
            var roleId = dbContext.UserRoles.FirstOrDefault(roleuser => roleuser.UserId == user.Id).RoleId;
            if (roleId != null)
            {
                role = dbContext.Roles.FirstOrDefault(rol => rol.Id == roleId);
            }
            return role;

        }
    }
}

