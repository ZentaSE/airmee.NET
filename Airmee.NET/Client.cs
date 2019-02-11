using System;
using System.Threading.Tasks;
using AirmeeDotNET.Classes;
using AirmeeDotNET.Communication;
using AirmeeDotNET.Constants.Enums;

namespace AirmeeDotNET
{
    public class Client
    {
        private readonly APICaller _apiCaller;
        private readonly string _defaultPlaceId;

        /// <summary>
        /// Create a client for Airmee API
        /// </summary>
        /// <param name="jwtToken">The long lived JWT of the place</param>
        /// <param name="placeId">Id of the pickup place from where the request originates</param>
        /// <param name="environment">States if this is Test or Live Environment</param>
        public Client(string jwtToken, string placeId, EnvironmentType environment)
        {
            _apiCaller = APICaller.CreateCaller(jwtToken, environment);
            _defaultPlaceId = placeId;
        }

        /// <summary>
        ///  This method queries the server and returns a list of available delivery times that the retailer can choose from
        /// </summary>
        /// <param name="country">ISO Alpha-2 Code - https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2</param>
        /// <param name="zipCode">The zipcode of the region that you want to check></param>
        /// <param name="firstDeliveryDay">Start date for the available windows</param>
        /// <returns>A collection of schedules for the customer to chose from based on default placeId</returns>
        public async Task<ScheduleCollection> GetDeliveryIntervalsAsync(string country, string zipCode, DateTime? firstDeliveryDay)
        {
            return await GetDeliveryIntervalsAsync(country, zipCode, _defaultPlaceId, firstDeliveryDay);
        }

        /// <summary>
        ///  This method queries the server and returns a list of available delivery times that the retailer can choose from
        /// </summary>
        /// <param name="country">ISO Alpha-2 Code - https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2</param>
        /// <param name="zipCode">The zipcode of the region that you want to check></param>
        /// <param name="firstDeliveryDay">Start date for the available windows</param>
        /// <param name="placeId">Id of the pickup place from where the request originates</param>
        /// <returns>A collection of schedules for the customer to chose from based on requested placeId</returns>
        public async Task<ScheduleCollection> GetDeliveryIntervalsAsync(string country, string zipCode, string placeId, DateTime? firstDeliveryDay)
        {
            return await _apiCaller.GetDeliveryIntervalsAsync(country, zipCode, placeId, firstDeliveryDay);
        }

        /// <summary>
        /// Check on the status of an Airmee delivery
        /// </summary>
        /// <param name="orderId">The id of the order you want to obtain the status for</param>
        /// <returns>Current status of delivery</returns>
        public async Task<OrderStatus> GetOrderStatusAsync(string orderId)
        {
            return await _apiCaller.GetOrderStatusAsync(orderId);
        }

        /// <summary>
        /// Get allowed maximum dimensions and weight for products that can be delivered
        /// </summary>
        /// <returns>Maximum allowed dimensions and weight for default placeId</returns>
        public async Task<ProductThresholdValues> GetProductThresholdsAsync()
        {
            return await GetProductThresholdsAsync(_defaultPlaceId);
        }

        /// <summary>
        /// Get allowed maximum dimensions and weight for products that can be delivered
        /// </summary>
        /// <param name="placeId">Id of the pickup place from where the request originates</param>
        /// <returns>Maximum allowed dimensions and weight for requested placeId</returns>
        public async Task<ProductThresholdValues> GetProductThresholdsAsync(string placeId)
        {
            return await _apiCaller.GetProductThresholdsAsync(placeId);
        }

        /// <summary>
        /// Place and order dor delivery
        /// </summary>
        /// <param name="order">The full orderdata</param>
        /// <returns>Tracking data</returns>

        public async Task<OrderTracking> RequestDeliveryAsync(Order order)
        {
            return await _apiCaller.SendOrderAsync(order);
        }
    }
}