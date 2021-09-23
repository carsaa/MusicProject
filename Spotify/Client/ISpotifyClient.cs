using Spotify.Models;
using System.Net;

namespace Spotify.Client
{
    public interface ISpotifyClient
    {
        (HttpStatusCode statusCode, SpotifyResponse responseObject) SearchForArtist(string artist);
    }
}