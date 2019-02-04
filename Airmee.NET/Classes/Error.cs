using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Error
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "extraMessage")]
        public string Details { get; set; }
    }
}