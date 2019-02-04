namespace AirmeeDotNET.Constants.Strings
{
    public class ErrorStrings
    {
        public const string InsufficientParamaters = "Insufficient parameters specified";
        public const string DeliveryCannotBeScheduled = "Delivery cannot be scheduled for the specified zip code and country parameters";
        public const string StatusCannotBeRequested = "Delivery status cannot be retrieved due to invalid order id";
        public const string PlaceIdDoesNotExist = "Place id does not exist in the database";
        public const string DeliveryCannotBeRequested = "Delivery cannot be requested due to invalid pickup place id";
        public const string AddressParsingError = "Address could not be parsed";
    }
}