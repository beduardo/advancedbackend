using System;
using Xunit;
using FluentAssertions;
using advancedbackend.domain.apiresponse.cityinfo;

namespace advancedbackend_tests.controllers
{
    public class CityMusicControllerTests
    {
        [Fact]
        public void ByName_CityFound_Success()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ByName_CityNotFound_Return404()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ByName_SeveralCity_Return404WithMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ByCoord_CityFound_Success()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ByCoord_CityNotFound_Return404()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void NoParameters_Return400WithMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ByCoord_InvalidCoord_Return400WithMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void SpotifyKeyExpired_Return500WithMessage()
        {
            //Token generate 401 status
            throw new NotImplementedException();
        }
    }
}