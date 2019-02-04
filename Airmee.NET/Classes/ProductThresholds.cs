using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class ProductThresholds
    {
        [JsonProperty(PropertyName = "threshold_values")]
        public ProductThresholdValues Values { get; set; }
    }
}