﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Spotify.Models
{
    public class SpotifyResponse
    {
        [JsonProperty("artists")]
        public Artists Artists { get; set; }
    }

    public class Artists
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Item
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    public class Image
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class Followers
    {
        [JsonProperty("href")]
        public object Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
