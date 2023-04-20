using JukeBoxPartyWeb.Models;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace JukeBoxPartyWeb.Controllers
{
    public class APICaller
    {
        private const string _url = "http://localhost:5003/api";

        public async static Task MakeTrackPlayed(int queueId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_url}/QueueElements/Play/{queueId}");
            
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }
        public async static Task<(HttpContent,bool)> AddTrackToQueue(Guid lobbyId, int songid, Guid userId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_url}/QueueElements");
            var values = new Dictionary<string, string>()
                 {
                     {"lobbyid", lobbyId.ToString()},
                     {"songid", songid.ToString()},
                     {"userId", userId.ToString()}
                 };
            string bodystring = JsonConvert.SerializeObject(values);

            var body = new StringContent(bodystring, null, "application/json");
            request.Content = body;
            var response = await client.SendAsync(request);
            return (response.Content,response.IsSuccessStatusCode);


        }
        public async static Task<List<Genre>> GetGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/Genres");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            
            return ConvertJsonToList<Genre>(result);

        }
        public async static Task<List<Lobby>> GetLobbies()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/Lobbies");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return ConvertJsonToList<Lobby>(result);

        }

        public async static Task PostSong(Song song) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_url}/Songs");
            var values = new Dictionary<string, string>()
                 {
                     {"title", song.Title},
                     {"url", song.URL},
                     {"artist", song.Artist},
                     {"duration", song.Duration.ToString(CultureInfo.InvariantCulture)},
                     {"genre", song.Genre}
                 }; 
            string bodystring = JsonConvert.SerializeObject(values);
            
            var body = new StringContent(bodystring  , null, "application/json");
            request.Content = body;
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();

            }catch(HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public static List<T> ConvertJsonToList<T>(string json)
        {
            var list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
    }
}
