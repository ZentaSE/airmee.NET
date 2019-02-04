using System.Collections.Generic;
using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class ScheduleCollection
    {
        [JsonProperty(PropertyName = "list_of_schedules")]
        public List<ScheduleGroup> Schedules { get; set; }
    }
}