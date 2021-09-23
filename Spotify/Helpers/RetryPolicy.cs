using Polly;
using System;
using System.Net;
using System.Net.Http;

namespace Spotify.Helpers
{
    public static class RetryPolicy
    {
        public static Policy<HttpResponseMessage> GetRetryPolicy()
        {
            var policy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetry(3, i => TimeSpan.FromSeconds(15));

            return policy;
        }
    }
}
