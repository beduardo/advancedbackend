using System;

namespace advancedbackend.services.exceptions {
    public class SpotifyTokenExpiredException : ApplicationException {
        public SpotifyTokenExpiredException():base("Spotify Token Expired")
        {
            
        }
    }
}