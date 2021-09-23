using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spotify.Helpers;
using Spotify.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Spotify.Client
{
    public class SpotifyClient : ISpotifyClient
    {
        private const string BASE_URL = "https://api.spotify.com/v1";
        private const string SEARCH_TYPE = "artist";
        private const string RESULTS_LIMIT = "1";

        private static HttpClient _client;
        private readonly ILogger<SpotifyClient> _logger;

        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }
                return _client;
            }
        }

        public SpotifyClient(ILogger<SpotifyClient> logger)
        {
            _logger = logger;
        }

        public (HttpStatusCode statusCode, SpotifyResponse responseObject) SearchForArtist(string artist)
        {
            try
            {
                _logger.LogInformation("Fetching access token from Spotify");
                var accessToken = TokenHelper.GetAccessToken();
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var url = new Uri($@"{BASE_URL}/search?q={artist}&type={SEARCH_TYPE}&limit={RESULTS_LIMIT}");

                _logger.LogInformation("Calling Spotify api");
                var retryPolicy = RetryPolicy.GetRetryPolicy();
                var response = retryPolicy.Execute(() => Client.GetAsync(url).Result);
                response.EnsureSuccessStatusCode();

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<SpotifyResponse>(jsonResponse);
                return (response.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception when calling Spotify");
                return (HttpStatusCode.InternalServerError, null);
            }
        }
    }
}
