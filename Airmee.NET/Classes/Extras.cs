using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Extras
    {
        [JsonProperty(PropertyName = "can_leave_outside_door")]
        public bool? CanLeaveOutsideDoor { get; set; }
    }
}