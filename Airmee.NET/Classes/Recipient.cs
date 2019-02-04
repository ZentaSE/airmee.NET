using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Recipient
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public long Phone { get; set; }

        [JsonProperty(PropertyName = "phone_number_country_code")]
        public int PhoneCountryCode { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}