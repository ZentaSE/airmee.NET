using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Checks
    {
        [JsonProperty(PropertyName = "min_age")]
        public int? MinimumAge { get; set; }

        [JsonProperty(PropertyName = "verify_id")]
        public bool? VerifyId { get; set; }

        [JsonProperty(PropertyName = "recipient_same_as_id")]
        public bool? RecipientMatchCustomer { get; set; }

        [JsonProperty(PropertyName = "take_signature")]
        public bool? SignForDelivery { get; set; }

        [JsonProperty(PropertyName = "take_photo")]
        public bool? DeliveryPhoto { get; set; }
    }
}