using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinalCapProject.Class
{
    public class PlaceList
    {
        [JsonPropertyName("post code")]
        public string PostCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
    }
}
