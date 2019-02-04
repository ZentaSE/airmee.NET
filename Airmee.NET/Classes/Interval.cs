using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Interval
    {
        [JsonProperty(PropertyName = "start")]
        public long Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public long End { get; set; }
    }
}