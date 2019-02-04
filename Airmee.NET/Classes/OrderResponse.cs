using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class OrderResponse
    {
        [JsonProperty(PropertyName = "order")]
        public OrderTracking Order { get; set; }
    }
}