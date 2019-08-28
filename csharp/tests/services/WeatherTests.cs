using System;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using Moq;
using advancedbackend.services;

namespace advancedbackend_tests.controllers
{
    public class WeatherTests
    {
        private Mock<IHttpClientFactory> clientFactoryMock;

        private WeatherService createTestObject() {
            clientFactoryMock = new Mock<IHttpClientFactory>();
            return new WeatherService(clientFactoryMock.Object);
        }

        [Fact]
        public void CreateObject() {
            var obt = createTestObject();
            obt.Should().NotBeNull();
        }

        // [Fact]
        public void GetTemperatureByCityName_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }
        // [Fact]
        public void GetTemperatureByCoords_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }
    }
}