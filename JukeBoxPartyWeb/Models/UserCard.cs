using System.Text.Json.Serialization;

namespace JukeBoxPartyWeb.Models
{
    public class UserCard
    {
        [JsonPropertyName("NickName")]
        public string NickName { get; set; }
        [JsonPropertyName("Url")]
        public string Url { get; set; }
    }
}
