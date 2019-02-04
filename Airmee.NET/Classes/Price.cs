using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Price
    {
        [JsonProperty(PropertyName = "amount")]
        public long Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }
}