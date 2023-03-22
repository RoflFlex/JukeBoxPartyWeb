using JukeBoxPartyWeb.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace JukeBoxPartyWeb.Controllers
{
    public class APICaller
    {
        private const string _url = "https://localhost:7283";

        public async static Task<List<Genre>> GetGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/api/Genres");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return ConvertJsonToList<Genre>(result);

        }

        public async static Task PostSong(Song song) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_url}/api/Songs");
            var values = new Dictionary<string, string>()
                 {
                     {"title", song.Title},
                     {"url", song.URL},
                     {"artist", song.Artist},
                     {"duration", song.Duration.ToString()},
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
