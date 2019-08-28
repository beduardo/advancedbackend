
using System.Collections.Generic;
using System.Threading.Tasks;
using advancedbackend.domain.config;
using advancedbackend.domain.responsemodel;
using Microsoft.Extensions.Options;

namespace advancedbackend.services
{
    public interface ICityMusicService
    {
        Task<CityTracks> GetTracksByCityName(string cityName);
        Task<CityTracks> GetTracksByCoords(float lat, float lon);
    }

    public class CityMusicService : ICityMusicService
    {
        private ISpotifyService Spotify;
        private IWeatherService Weather;

        private IOptions<AppSettings> Config;

        public CityMusicService(IWeatherService weather, ISpotifyService spotify, IOptions<AppSettings> config)
        {
            Weather = weather;
            Spotify = spotify;
            Config = config;
        }

        private async Task<CityTracks> tracksFromTemp(double temperature)
        {
            var token = await Spotify.GetToken(Config.Value.spotify_id, Config.Value.spotify_secret);

            var ret = new CityTracks
            {
                Temperature = temperature
            };

            if (temperature > 30)
            {
                ret.Tracks = await Spotify.GetPartyTracks(token.AccessToken);
                ret.Type = "Party";
            }
            else if (temperature >= 15 && temperature <= 30)
            {
                ret.Tracks = await Spotify.GetPopTracks(token.AccessToken);
                ret.Type = "Pop";
            }
            else if (temperature >= 10 && temperature <= 14)
            {
                ret.Tracks = await Spotify.GetRockTracks(token.AccessToken);
                ret.Type = "Rock";
            }
            else
            {
                ret.Tracks = await Spotify.GetClassicalTracks(token.AccessToken);
                ret.Type = "Clasical";
            }

            return ret;
        }

        public async Task<CityTracks> GetTracksByCityName(string cityName)
        {
            var temperature = await Weather.GetTemperatureByCityName(cityName);
            return await tracksFromTemp(temperature);
        }

        public async Task<CityTracks> GetTracksByCoords(float lat, float lon)
        {
            var temperature = await Weather.GetTemperatureByCoord(lat, lon);
            return await tracksFromTemp(temperature);
        }
    }
}