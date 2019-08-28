
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using advancedbackend.domain.apiresponse.spotify;
using advancedbackend.domain.apiresponse.spotify.playlisttracks;
using advancedbackend.services.exceptions;
using advancedbackend.domain.apiresponse.spotify.playlists;

namespace advancedbackend.services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetPartyTracks(string token);
        Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetRockTracks(string token);
        Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetPopTracks(string token);
        Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetClassicalTracks(string token);
        Task<AuthorizationToken> GetToken(string clientId, string clientSecret);
    }

    public class SpotifyService : ISpotifyService
    {
        IHttpClientFactoryWrapper ClientFactory;
        IBase64 Base64;
        public SpotifyService(IHttpClientFactoryWrapper clientFactory, IBase64 base64)
        {
            ClientFactory = clientFactory;
            Base64 = base64;
        }

        public async Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetClassicalTracks(string token)
        {
            var tracks = await GetTracks(token, "mood");
            return tracks.Items.Select(t => new advancedbackend.domain.responsemodel.Track {
                Name = t.Track.Name
            });
        }

        public async Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetPopTracks(string token)
        {
            var tracks = await GetTracks(token, "pop");
            return tracks.Items.Select(t => new advancedbackend.domain.responsemodel.Track {
                Name = t.Track.Name
            });
        }

        public async Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetRockTracks(string token)
        {
            var tracks = await GetTracks(token, "rock");
            return tracks.Items.Select(t => new advancedbackend.domain.responsemodel.Track {
                Name = t.Track.Name
            });
        }

        public async Task<AuthorizationToken> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            var author = Base64.Base64Encode($"{clientId}:{clientSecret}");
            request.Headers.Add("Authorization", $"Basic {author}");
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            request.Content = new FormUrlEncodedContent(nvc);

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content
                    .ReadAsStringAsync();
                return AuthorizationToken.FromJson(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new SpotifyTokenExpiredException();
            } else {
                throw new ApplicationException(response.StatusCode.ToString());
            }
        }

        public async Task<IEnumerable<advancedbackend.domain.responsemodel.Track>> GetPartyTracks(string token)
        {
            var tracks = await GetTracks(token, "party");
            return tracks.Items.Select(t => new advancedbackend.domain.responsemodel.Track {
                Name = t.Track.Name
            });
        }

        private async Task<PlaylistTracks> GetTracks(string token, string playlistid) {

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/browse/categories/{playlistid}/playlists");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var client = ClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json =  await response.Content.ReadAsStringAsync();
                var playlists = Playlists.FromJson(json);
                var firstList = playlists.PlaylistsPlaylists.Items.FirstOrDefault();
                if (firstList != null) {
                    request = new HttpRequestMessage(HttpMethod.Get, firstList.Tracks.Href);
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    response = await client.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();
                    return PlaylistTracks.FromJson(json);
                }

                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new SpotifyTokenExpiredException();
            } else {
                throw new ApplicationException(response.StatusCode.ToString());
            }
        }
    }
}