using System.Collections.Generic;

namespace advancedbackend.domain.responsemodel {
    public class CityTracks {
        public double Temperature { get; set; }
        public string Type { get; set; }
        public IEnumerable<Track> Tracks { get; set; }
    }
}