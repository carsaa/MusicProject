using MusicService.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicService.Models;
using Spotify.Client;
using Spotify.Models;
using System.Linq;
using System.Net;

namespace MusicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase 
    {
        private readonly ISpotifyClient _client;

        public ArtistsController(ISpotifyClient client)
        {
            _client = client;
        }

        [HttpGet]
        public ActionResult<ArtistModel> Get([FromQuery(Name = "search")] string searchString)
        {
            var (statusCode, spotifyResponse) = _client.SearchForArtist(searchString);

            if (statusCode == HttpStatusCode.OK
                && spotifyResponse != null)
            {
                if (spotifyResponse.Artists.Items.Count == 1 && ResponseValidator.IsMatch(searchString, spotifyResponse.Artists.Items[0].Name))
                {
                    return CreateModel(spotifyResponse);
                }
                return NotFound("No match");
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        private ArtistModel CreateModel(SpotifyResponse response)
        {
            var artistResult = response.Artists.Items.Single();

            return new ArtistModel
            {
                Id = artistResult.Id,
                Name = artistResult.Name,
            };
        }
    }
}