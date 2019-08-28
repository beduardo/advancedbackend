using System;
using Xunit;
using FluentAssertions;
using advancedbackend.domain.apiresponse.cityinfo;
using advancedbackend.controllers;
using advancedbackend.domain.config;
using Moq;
using Microsoft.Extensions.Options;
using advancedbackend.services;
using advancedbackend.domain.responsemodel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using advancedbackend.services.exceptions;

namespace advancedbackend_tests.controllers
{
    public class CityMusicControllerTests
    {   
        private AppSettings config;
        private Mock<ICityMusicService> serviceMock;

        private CityMusicController CreateTestObject() {
            //Create mock dependencies
            var opt = new Mock<IOptions<AppSettings>>();
            config = new AppSettings();
            opt.Setup(m => m.Value).Returns(config);

            serviceMock = new Mock<ICityMusicService>();

            //Return object
            return new CityMusicController(opt.Object, serviceMock.Object);
        }

        [Fact]
        public void CreateObject() {
            var obt = CreateTestObject();
            obt.Should().NotBeNull();
        }


        [Theory]
        [InlineData("cityname")]
        [InlineData("cityname2")]
        [InlineData("cityname3")]
        public void ByName_CallCorrectMethodOfDependency(string city)
        {
            //Prepare
            var obt = CreateTestObject();

            //Execute
            obt.Get(city, 0, 0);

            //Verify
            serviceMock.Verify(m => m.GetTracksByCityName(city), Times.AtLeastOnce());
        }

        [Fact]
        public void ByName_CityNameA_GetCorrectResponse()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCityName("cityA")).Returns(new[] {
                new Track { Name = "trackA1" },
                new Track { Name = "trackA2" },
                new Track { Name = "trackA3" },
            });

            //Execute
            var resp = obt.Get("cityA", 0, 0);

            //Verify
            resp.Should().BeOfType<OkObjectResult>();
            var respTyped = (OkObjectResult)resp;
            respTyped.Value.Should().BeEquivalentTo(new[] {
                new Track { Name = "trackA1" },
                new Track { Name = "trackA2" },
                new Track { Name = "trackA3" },
            });

        }

        [Fact]
        public void ByName_CityNameB_GetCorrectResponse()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCityName("cityB")).Returns(new[] {
                new Track { Name = "trackB1" },
                new Track { Name = "trackB2" },
                new Track { Name = "trackB3" },
            });

            //Execute
            var resp = obt.Get("cityB", 0, 0);

            //Verify
            resp.Should().BeOfType<OkObjectResult>();
            var respTyped = (OkObjectResult)resp;
            respTyped.Value.Should().BeEquivalentTo(new[] {
                new Track { Name = "trackB1" },
                new Track { Name = "trackB2" },
                new Track { Name = "trackB3" },
            });

        }

        [Theory]
        [InlineData(10, 11)]
        [InlineData(30, -15.2)]
        [InlineData(-40.3, -16.23)]
        public void ByCoord_CallCorrectMethodOfDependency(float lat, float lon)
        {
            //Prepare
            var obt = CreateTestObject();

            //Execute
            obt.Get(null, lat, lon);

            //Verify
            serviceMock.Verify(m => m.GetTracksByCoords(lat, lon), Times.AtLeastOnce());
        }

        [Fact]
        public void ByName_CityFound_Success()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCityName("cityX")).Returns(new Track[] {});

            //Execute
            var resp = obt.Get("cityX", 0, 0);

            //Verify
            resp.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ByName_CityNotFound_Return404()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCityName("cityX")).Returns((IEnumerable<Track>)null);

            //Execute
            var resp = obt.Get("cityX", 0, 0);

            //Verify
            resp.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void ByCoord_CityFound_Success()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCoords(10, 11)).Returns(new Track[] {});

            //Execute
            var resp = obt.Get(null, 10, 11);

            //Verify
            resp.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ByCoord_CityNotFound_Return404()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCoords(8, 13)).Returns((IEnumerable<Track>)null);

            //Execute
            var resp = obt.Get(null, 8, 13);

            //Verify
            resp.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void NoParameters_Return400WithMessage()
        {
            //Prepare
            var obt = CreateTestObject();

            //Execute
            var resp = obt.Get(null, 0, 0);

            //Verify
            resp.Should().BeOfType<BadRequestObjectResult>();
            var respTyped = (BadRequestObjectResult)resp;
            
            respTyped.Value.Should().BeEquivalentTo(new ErrorMessageResponse {
                Error = "BadRequest",
                Message = "Invalid parameters"
            });
        }

        [Fact]
        public void ByName_Exception_Return500WithMessage()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCityName(It.IsAny<string>())).Throws(new Exception("CustomMessage"));

            //Execute
            var resp = obt.Get("city", 0, 0);

            //Verify
            resp.Should().BeOfType<ObjectResult>();
            var respTyped = (ObjectResult)resp;

            respTyped.StatusCode.Should().Be(500);

            respTyped.Value.Should().BeEquivalentTo(new ErrorMessageResponse {
                Error = "InternalServerError",
                Message = "CustomMessage"
            }, opt => opt.Excluding(p => p.Trace) );
        }

        [Fact]
        public void ByCoord_Exception_Return500WithMessage()
        {
            //Prepare
            var obt = CreateTestObject();

            serviceMock.Setup(m => m.GetTracksByCoords(It.IsAny<float>(), It.IsAny<float>())).Throws(new Exception("CustomMessage2"));

            //Execute
            var resp = obt.Get(null, 10, 10);

            //Verify
            resp.Should().BeOfType<ObjectResult>();
            var respTyped = (ObjectResult)resp;

            respTyped.StatusCode.Should().Be(500);

            respTyped.Value.Should().BeEquivalentTo(new ErrorMessageResponse {
                Error = "InternalServerError",
                Message = "CustomMessage2"
            }, opt => opt.Excluding(p => p.Trace) );
        }
    }
}