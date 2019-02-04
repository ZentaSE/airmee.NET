using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Item
    {
        [JsonProperty(PropertyName = "length")]
        public double Length { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public double Weight { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "parcel_id")]
        public string ParcelId { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public Price UnitPrice { get; set; }

        [JsonProperty(PropertyName = "quanity")]
        public int Quantity { get; set; }
    }
}