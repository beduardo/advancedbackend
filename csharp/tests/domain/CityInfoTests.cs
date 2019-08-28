using System;
using Xunit;
using FluentAssertions;
using advancedbackend.domain.apiresponse.cityinfo;

namespace advancedbackend_tests.domain
{
    public class CityInfoTests
    {
        [Fact]
        public void FromJson()
        {
            //Prepare
            var obt = CityInfo.FromJson(jsonTest);

            //Verify
            obt.Should().BeEquivalentTo(new CityInfo{
                Coord = new Coord {
                    Lat = -18.92,
                    Lon = -48.28
                }, 
                Weather = new[] {
                        new Weather {
                        Description = "clear sky",
                        Icon = "01n",
                        Id = 800L,
                        Main = "Clear"
                    }
                },
                Base = "stations",
                Main = new Main {
                    Humidity = 49L,
                    Pressure = 1023L,
                    Temp = 294.15,
                    TempMax = 294.15,
                    TempMin = 294.15
                },
                Visibility = 10000L,
                Wind = new Wind {
                    Deg = 30L,
                    Speed = 3.1
                },
                Clouds = new Clouds {
                    All = 0L
                },
                Dt = 1566949302L,
                Sys = new Sys {
                    Country = "BR",
                    Id = 8471L,
                    Message = 0.0063,
                    Sunrise = 1566897924L,
                    Sunset = 1566939870L,
                    Type = 1L
                },
                Timezone = -10800L,
                Id = 3445831L,
                Name = "Uberlandia",
                Cod = 200L
            });
        }


        //JSON Test
        private string jsonTest = @"{
    'coord': {
        'lon': -48.28,
        'lat': -18.92
    },
    'weather': [
        {
            'id': 800,
            'main': 'Clear',
            'description': 'clear sky',
            'icon': '01n'
        }
    ],
    'base': 'stations',
    'main': {
        'temp': 294.15,
        'pressure': 1023,
        'humidity': 49,
        'temp_min': 294.15,
        'temp_max': 294.15
    },
    'visibility': 10000,
    'wind': {
        'speed': 3.1,
        'deg': 30
    },
    'clouds': {
        'all': 0
    },
    'dt': 1566949302,
    'sys': {
        'type': 1,
        'id': 8471,
        'message': 0.0063,
        'country': 'BR',
        'sunrise': 1566897924,
        'sunset': 1566939870
    },
    'timezone': -10800,
    'id': 3445831,
    'name': 'Uberlandia',
    'cod': 200
}";
    }
}
