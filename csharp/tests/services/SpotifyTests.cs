using System;
using Xunit;
using FluentAssertions;
using advancedbackend.domain.apiresponse.cityinfo;
using advancedbackend.services;
using Moq;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using advancedbackend.domain.apiresponse.spotify;

namespace advancedbackend_tests.controllers
{
    public class SpotifyTests
    {

        private Mock<IHttpClientFactoryWrapper> clientFactoryMock;
        private Mock<IBase64> base64Mock;

        private SpotifyService createTestObject() {
            clientFactoryMock = new Mock<IHttpClientFactoryWrapper>();
            base64Mock = new Mock<IBase64>();
            return new SpotifyService(clientFactoryMock.Object, base64Mock.Object);
        }

        [Fact]
        public void CreateObject() {
            var obt = createTestObject();
            obt.Should().NotBeNull();
        }

        [Theory]
        [InlineData("id1", "secret1", "BQAcOJJ09bI0U_u")]
        [InlineData("id2", "secret2", "SBUicnxrvfo2gwk")]
        [InlineData("id3", "secret3", "A39T8_8sAsCcztDu")]
        public async Task GetToken_GetTokenCorrectly(string id, string secret, string expectedBase64)
        {
            //prepare
            var obt = createTestObject();

            var httpClientMock = new Mock<IHttpClientWrapper>();
            clientFactoryMock.Setup(m => m.CreateClient()).Returns(httpClientMock.Object);

            var resp = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            resp.Content = new StringContent(jsonToken, System.Text.Encoding.UTF8, "application/json");
            httpClientMock.Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(resp);

            // execute
            var token = await obt.GetToken(id, secret);

            //Verify
            var expectedRequest = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            expectedRequest.Headers.Add("Authorization", $"Basic {expectedBase64}");
            // expectedRequest.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            expectedRequest.Content = new FormUrlEncodedContent(nvc);
            httpClientMock.Verify(m => m.SendAsync(expectedRequest));

            token.Should().BeEquivalentTo(AuthorizationToken.FromJson(jsonToken));

            base64Mock.Verify(m => m.Base64Encode($"{id}:{secret}"));
        }

        // [Fact]
        public void GetPartyTracks_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void GetPartyTracks_Error401_ExpiredTokenException()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void GetPopTracks_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }
        
        // [Fact]
        public void GetPopTracks_Error401_ExpiredTokenException()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void GetRockTracks_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }
        
        // [Fact]
        public void GetRockTracks_Error401_ExpiredTokenException()
        {
            throw new NotImplementedException();
        }

        // [Fact]
        public void GetClassicalTracks_CallCorrectUrl()
        {
            throw new NotImplementedException();
        }
        
        // [Fact]
        public void GetClassicalTracks_Error401_ExpiredTokenException()
        {
            throw new NotImplementedException();
        }

        private string jsonToken = @"{
            'access_token': 'BQAcOJJ09bI0U_u0ZYZSBUicnxrvfo2gwklTroiD5A39T8_8sAsCcztDu6IAQ8pNLX0gJNnrfpzcq-rwvUE',
            'token_type': 'Bearer',
            'expires_in': 3600,
            'scope': ''
        }";
        

        private string jsonTracks = @"{
            'playlists': {
                'href': 'https://api.spotify.com/v1/browse/categories/party/playlists?offset=0&limit=20',
                'items': [
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DX1TEroFz7Oja'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX1TEroFz7Oja',
                        'id': '37i9dQZF1DX1TEroFz7Oja',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/80a1acbe09a4459c23208664663c785ee5d93f97',
                                'width': null
                            }
                        ],
                        'name': 'New Year's Party Mix',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU0ODc1MDY5NCwwMDAwMDAwMzAwMDAwMTY3ZDAwNGFmZDIwMDAwMDE2ODk4YmMyNzZm',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX1TEroFz7Oja/tracks',
                            'total': 95
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DX1TEroFz7Oja'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DXaXB8fQg7xif'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DXaXB8fQg7xif',
                        'id': '37i9dQZF1DXaXB8fQg7xif',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/288ac734751abee8e029325a254d687ba3e70ed9',
                                'width': null
                            }
                        ],
                        'name': 'Dance Party',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2Njk4NzIzMiwwMDAwMDAwMGQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0Mjdl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DXaXB8fQg7xif/tracks',
                            'total': 150
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DXaXB8fQg7xif'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DX8FwnYE6PRvL'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX8FwnYE6PRvL',
                        'id': '37i9dQZF1DX8FwnYE6PRvL',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/956c45784537ebe8136dbc4c362305b383e54e8d',
                                'width': null
                            }
                        ],
                        'name': 'Rock Party',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2Njk4NzE4MywwMDAwMDAwMGQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0Mjdl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX8FwnYE6PRvL/tracks',
                            'total': 50
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DX8FwnYE6PRvL'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DWVcbzTgVpNRm'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DWVcbzTgVpNRm',
                        'id': '37i9dQZF1DWVcbzTgVpNRm',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/99de0f475f84b7df97d90fedb1fa26be78163e3e',
                                'width': null
                            }
                        ],
                        'name': 'Latin Party Anthems',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2Njk4NzIzNSwwMDAwMDAwMGQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0Mjdl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DWVcbzTgVpNRm/tracks',
                            'total': 75
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DWVcbzTgVpNRm'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DX0Uv9tZ47pWo'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX0Uv9tZ47pWo',
                        'id': '37i9dQZF1DX0Uv9tZ47pWo',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/22e6af27895fd4091b1e5ef80bd1c1a30a434490',
                                'width': null
                            }
                        ],
                        'name': 'Girls' Night',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2Njk4NzE4MiwwMDAwMDAwMGQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0Mjdl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX0Uv9tZ47pWo/tracks',
                            'total': 65
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DX0Uv9tZ47pWo'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DX8a1tdzq5tbM'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX8a1tdzq5tbM',
                        'id': '37i9dQZF1DX8a1tdzq5tbM',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/27893a18d1688398fa4634edd1196a8bfff6020d',
                                'width': null
                            }
                        ],
                        'name': 'Dance Classics',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2Njk4NzE4MSwwMDAwMDAwMGQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0Mjdl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX8a1tdzq5tbM/tracks',
                            'total': 130
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DX8a1tdzq5tbM'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DWUq3wF0JVtEy'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DWUq3wF0JVtEy',
                        'id': '37i9dQZF1DWUq3wF0JVtEy',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/6325b2cde49483d2b4295d65a0ab1f893c97cf84',
                                'width': null
                            }
                        ],
                        'name': 'Shuffle Syndrome',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2NTM2NjYyNCwwMDAwMDA3MjAwMDAwMTZjNzcxZjIxZDUwMDAwMDE2ODk5NzU2ZjYw',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DWUq3wF0JVtEy/tracks',
                            'total': 247
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DWUq3wF0JVtEy'
                    },
                    {
                        'collaborative': false,
                        'external_urls': {
                            'spotify': 'https://open.spotify.com/playlist/37i9dQZF1DX2W6AhhHuQN4'
                        },
                        'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX2W6AhhHuQN4',
                        'id': '37i9dQZF1DX2W6AhhHuQN4',
                        'images': [
                            {
                                'height': null,
                                'url': 'https://pl.scdn.co/images/pl/default/0a23bd11cbb4e1f27d39ee379b692077f288c8d3',
                                'width': null
                            }
                        ],
                        'name': 'In a Past Life',
                        'owner': {
                            'display_name': 'Spotify',
                            'external_urls': {
                                'spotify': 'https://open.spotify.com/user/spotify'
                            },
                            'href': 'https://api.spotify.com/v1/users/spotify',
                            'id': 'spotify',
                            'type': 'user',
                            'uri': 'spotify:user:spotify'
                        },
                        'primary_color': null,
                        'public': null,
                        'snapshot_id': 'MTU2NTIwMzA4NywwMDAwMDAyNDAwMDAwMTZjNmQ1ZmMwNzAwMDAwMDE2YThkYzk1YjBl',
                        'tracks': {
                            'href': 'https://api.spotify.com/v1/playlists/37i9dQZF1DX2W6AhhHuQN4/tracks',
                            'total': 133
                        },
                        'type': 'playlist',
                        'uri': 'spotify:playlist:37i9dQZF1DX2W6AhhHuQN4'
                    }
                ],
                'limit': 20,
                'next': null,
                'offset': 0,
                'previous': null,
                'total': 8
            }
        }";

    }
}    
    