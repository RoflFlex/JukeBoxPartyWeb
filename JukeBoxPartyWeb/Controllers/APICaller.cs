using JukeBoxPartyWeb.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace JukeBoxPartyWeb.Controllers
{
    public class APICaller
    {


        public async static Task<List<Genre>> GetGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7283/api/Genres");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return ConvertJsonToList<Genre>(result);

        }

        public async static Task PostSong(Song song) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7283/api/Songs");
            var values = new Dictionary<string, string>()
                 {
                     {"title", song.Title},
                     {"url", song.URL},
                     {"artist", song.Artist},
                     {"genre", song.Genre}
                 };
            //var content = new FormUrlEncodedContent(values);
            /*var content = new MultipartFormDataContent();
            content.Add(new StringContent("title"), song.Title, "application/json");
            content.Add(new StringContent("url"), song.URL,  "application/json");
            content.Add(new StringContent("artist"), song.Artist , "application/json");
            content.Add(new StringContent("genre"), song.Genre, "application/json");
            request.Content = content;*/
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
