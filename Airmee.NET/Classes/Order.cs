using System.Collections.Generic;
using Newtonsoft.Json;

namespace AirmeeDotNET.Classes
{
    public class Order
    {
        [JsonProperty(PropertyName = "place_id")]
        public string PlaceId { get; set; }

        [JsonProperty(PropertyName = "recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty(PropertyName = "ecomm_id")]
        public string EcommId { get; set; }

        [JsonProperty(PropertyName = "dropoff_address")]
        public Address DropoffAddress { get; set; }

        [JsonProperty(PropertyName = "message_for_courier")]
        public string CourierMessage { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }

        [JsonProperty(PropertyName = "pickup_interval")]
        public Interval PickupInterval { get; set; }

        [JsonProperty(PropertyName = "dropoff_interval")]
        public Interval DropoffInterval { get; set; }

        [JsonProperty(PropertyName = "checks")]
        public Checks Checks { get; set; }

        [JsonProperty(PropertyName = "extas")]
        public Extras Extras { get; set; }
    }
}