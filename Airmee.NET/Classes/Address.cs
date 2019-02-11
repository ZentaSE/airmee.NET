using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Address
    {
        [JsonProperty(PropertyName = "street_and_number")]
        public string StreetAddress { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "apartment")]
        public string ApartmentNumber { get; set; }

        [JsonProperty(PropertyName = "floor")]
        public string Floor { get; set; }

        [JsonProperty(PropertyName = "door_code")]
        public string DoorCode { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }
    }
}