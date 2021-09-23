using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Spotify.Helpers
{
    public static class TokenHelper
    {
        private const string TOKEN_URL = "https://accounts.spotify.com/api/token";
        private const string CLIENT_ID = "b4821faf687d4137ae12b735034ab9fd";
        private const string CLIENT_SECRET = "8d1bc161e3ca4267ba35d2efb710f3c6";

        private static string Token = null;
        private static DateTime ExpirationTime;
        private static HttpClient _client;

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

        public static string GetAccessToken()
        {
            if (Token != null && ExpirationTime > DateTime.UtcNow)
            {
                return Token;
            }

            var authorization = Encoding.ASCII.GetBytes($@"{CLIENT_ID}:{CLIENT_SECRET}");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authorization));

            var content = new Dictionary<string, string>
                {
                    { "Content-Type", "application/x-www-form-urlencoded" },
                    { "grant_type", "client_credentials" },
                };
            var encodedContent = new FormUrlEncodedContent(content);

            var retryPolicy = RetryPolicy.GetRetryPolicy();
            var response = retryPolicy.Execute(() => Client.PostAsync(TOKEN_URL, encodedContent).Result);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch access token");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<AccessToken>(jsonResponse);

            Token = token.Token;
            ExpirationTime = DateTime.UtcNow.AddSeconds(token.ExpiresIn).AddSeconds(-30);

            return Token;
        }
    }

    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
