using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AirmeeDotNET.Classes;
using AirmeeDotNET.Constants.Enums;
using AirmeeDotNET.Constants.Strings;
using AirmeeDotNET.Exceptions;
using Newtonsoft.Json;
using static System.String;

namespace AirmeeDotNET.Communication
{
    internal class APICaller
    {
        private HttpClient _client;

        internal static APICaller CreateCaller(string jwtToken, EnvironmentType environment)
        {
            var caller = new APICaller();
            caller.Initialize(jwtToken, environment);

            return caller;
        }

        internal async Task<ScheduleCollection> GetDeliveryIntervalsAsync(string country, string zipCode, string placeId, DateTime? firstDeliveryDay)
        {
            var parameters = new NameValueCollection
                             {
                                 {"country", country}, {"zip_code", zipCode}, {"place_id", placeId}, {"date", firstDeliveryDay?.Date.ToString("yyyy-MM-dd")}
                             };

            return firstDeliveryDay != null
                ? JsonConvert.DeserializeObject<ScheduleCollection>(await CallAsync("checkout_delivery_intervals_for_zip_code", Method.Get, parameters))
                : JsonConvert.DeserializeObject<ScheduleCollection>(await CallAsync("delivery_intervals_for_zip_code", Method.Get, parameters));
        }

        internal async Task<OrderStatus> GetOrderStatusAsync(string orderId)
        {
            return JsonConvert.DeserializeObject<OrderStatus>(await CallAsync("order_status", Method.Get, new NameValueCollection {{"order_Id", orderId}}));
        }

        internal async Task<ProductThresholdValues> GetProductThresholdsAsync(string placeId)
        {
            return JsonConvert
                   .DeserializeObject<ProductThresholds>(await CallAsync("product_threshold_for_place", Method.Get,
                                                                         new NameValueCollection {{"place_id", placeId}})).Values;
        }

        internal async Task<OrderTracking> SendOrderAsync(Order order)
        {
            return JsonConvert
                   .DeserializeObject<OrderResponse>(await CallAsync("request_delivery", Method.Post, new NameValueCollection(),
                                                                     JsonConvert.SerializeObject(order))).Order;
        }

        private async Task<string> CallAsync(string endpoint, string method, NameValueCollection additionalParameters, string payload = "")
        {
            HttpResponseMessage response;
            var requestUri = $"{endpoint}?{CreateQueryString(additionalParameters)}";

            switch (method)
            {
                case Method.Get:
                    response = await _client.GetAsync(requestUri);
                    break;

                case Method.Post:
                    response = await _client.PostAsync(requestUri, new StringContent(payload, Encoding.UTF8, "application/json"));
                    break;

                default:
                    throw new ArgumentException($"Method {method} is not supported");
            }

            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorAsync(response);
            }

            return await response.Content.ReadAsStringAsync();
        }

        private static async Task HandleErrorAsync(HttpResponseMessage response)
        {
            var error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());

            if ((int) response.StatusCode >= 500)
            {
                throw new ServerError(error.Details);
            }

            switch (error.Message)
            {
                case ErrorStrings.InsufficientParamaters:
                    throw new InsufficientParametersException(error.Details);

                case ErrorStrings.AddressParsingError:
                    throw new AddressException(error.Details);

                case ErrorStrings.DeliveryCannotBeRequested:
                    throw new DeliveryException(error.Details);

                case ErrorStrings.DeliveryCannotBeScheduled:
                    throw new DeliveryException(error.Details);

                case ErrorStrings.PlaceIdDoesNotExist:
                    throw new InvalidParametersException(error.Details);

                default:
                    throw new Exception(error.Details);
            }
        }

        private static string CreateQueryString(NameValueCollection collection)
        {
            return Join("&", collection.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(collection[a])));
        }

        private void Initialize(string jwtToken, EnvironmentType environment)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            _client.BaseAddress = new Uri(environment == EnvironmentType.Live
                                              ? "https://api.airmee.com/integration/"
                                              : "https://staging-api.airmee.com/integration/");
        }
    }
}