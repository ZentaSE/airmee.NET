using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class OrderTracking
    {
        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "tracking_url")]
        public string TrackingUrl { get; set; }
    }
}