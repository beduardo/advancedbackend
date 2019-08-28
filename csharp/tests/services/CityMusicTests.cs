// If temperature (celcius) is above 30 degrees, suggest tracks for party
// In case temperature is between 15 and 30 degrees, suggest pop music tracks
// If it's a bit chilly (between 10 and 14 degrees), suggest rock music tracks
// Otherwise, if it's freezing outside, suggests classical music tracks

using System;
using Xunit;
using FluentAssertions;
using advancedbackend.domain.apiresponse.cityinfo;
using advancedbackend.services;
using Moq;

namespace advancedbackend_tests.controllers
{
    public class CityMusicTests
    {
        private Mock<ISpotifyService> spotifyMock;
        private Mock<IWeatherService> weatherMock;

        private CityMusicService createTestObject() {
            weatherMock = new Mock<IWeatherService>();
            spotifyMock = new Mock<ISpotifyService>();
            return new CityMusicService(weatherMock.Object, spotifyMock.Object);
        }


        [Fact]
        public void CreateObject() {
            var obt = createTestObject();
            obt.Should().NotBeNull();
        }

        // [Fact]
        public void TemperatureAbove30_GetPartyTracks()
        {

        }

        // [Fact]
        public void TemperatureBetweem15And30_GetPopTracks()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void TemperatureBetweem10And14_GetRockTracks()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void TemperatureBelow10_GetClassicalTracks()
        {
            throw new NotImplementedException();
        }
    }
}