using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class ProductThresholdValues
    {
        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "length")]
        public double Lenght { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public double Weight { get; set; }
    }
}