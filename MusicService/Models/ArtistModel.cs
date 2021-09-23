using Spotify.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicService.Models
{
    public class ArtistModel
    {
        [JsonPropertyName("artist_id")]
        public string Id { get; set; }

        [JsonPropertyName("artist_name")]
        public string Name { get; set; }
    }
}
