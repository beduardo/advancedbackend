namespace advancedbackend.domain.apiresponse.spotify
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AuthorizationToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }

    public partial class AuthorizationToken
    {
        public static AuthorizationToken FromJson(string json) => JsonConvert.DeserializeObject<AuthorizationToken>(json, advancedbackend.domain.apiresponse.spotify.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AuthorizationToken self) => JsonConvert.SerializeObject(self, advancedbackend.domain.apiresponse.spotify.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
