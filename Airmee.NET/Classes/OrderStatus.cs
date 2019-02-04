using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class OrderStatus
    {
        [JsonProperty(PropertyName = "order_short_status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "order_long_status")]
        public string StatusLong { get; set; }
    }
}