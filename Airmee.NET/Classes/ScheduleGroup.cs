using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class ScheduleGroup
    {
        [JsonProperty(PropertyName = "pickup_interval")]
        public Schedule PickupInterval { get; set; }

        [JsonProperty(PropertyName = "dropoff_interval")]
        public Schedule DropoffInterval { get; set; }
    }
}