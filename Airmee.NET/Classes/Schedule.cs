using System;
using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Schedule
    {
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public int End { get; set; }

        [JsonProperty(PropertyName = "formatted_as_schedule")]
        public string Readable { get; set; }

        [JsonIgnore]
        public DateTime StartTimeUtc => BaseDate.AddSeconds(Start);

        [JsonIgnore]
        public DateTime EndTimeUtc => BaseDate.AddSeconds(End);

        [JsonIgnore]
        private DateTime BaseDate => new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }
}