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
    public class APICaller
    {
        private readonly HttpClient _client;

        public APICaller(string jwtToken, EnvironmentType environment)
        {
            _client = new HttpClient();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);
            _client.BaseAddress = new Uri(environment == EnvironmentType.Live ? "https://api.airmee.com/integration/" : "https://staging-api.airmee.com/integration/");
        }

        public async Task<ScheduleCollection> GetDeliveryIntervalsAsync(string country, string zipCode, string placeId, DateTime? firstDeliveryDay)
        {
            var paramaters = new NameValueCollection
                             {
                                 {
                                     "country", country
                                 },
                                 {
                                     "zip_code", zipCode
                                 },
                                 {
                                     "place_id", placeId
                                 },
                                 {
                                     "date", firstDeliveryDay?.Date.ToString("yyyy-MM-dd")
                                 }
                             };
            if (firstDeliveryDay != null)
            {
                return JsonConvert.DeserializeObject<ScheduleCollection>(await CallAsync("checkout_delivery_intervals_for_zip_code", Method.Get, paramaters));
            }

            return JsonConvert.DeserializeObject<ScheduleCollection>(await CallAsync("delivery_intervals_for_zip_code", Method.Get, paramaters));
        }

        public async Task<OrderStatus> GetOrderStatusAsync(string orderId)
        {
            var paramaters = new NameValueCollection
                             {
                                 {
                                     "order_Id", orderId
                                 }
                             };

            return JsonConvert.DeserializeObject<OrderStatus>(await CallAsync("order_status", Method.Get, paramaters));
        }

        public async Task<ProductThresholdValues> GetProductThresholdsAsync(string placeId)
        {
            var paramaters = new NameValueCollection
                             {
                                 {
                                     "place_id", placeId
                                 }
                             };

            return JsonConvert.DeserializeObject<ProductThresholds>(await CallAsync("product_threshold_for_place", Method.Get, paramaters)).Values;
        }

        public async Task<OrderTracking> SendOrderAsync(Order order)
        {
            var paramaters = new NameValueCollection();

            return JsonConvert.DeserializeObject<OrderResponse>(await CallAsync("request_delivery", Method.Post, paramaters, JsonConvert.SerializeObject(order))).Order;
        }

        private async Task<string> CallAsync(string endpoint, string method, NameValueCollection additionalParamaters, string payload = "")
        {
            var queryParams = Join("&", additionalParamaters.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(additionalParamaters[a])));

            var response = new HttpResponseMessage();
            switch (method)
            {
                case Method.Get:
                    response = await _client.GetAsync($"{endpoint}?{queryParams}");
                    if (!response.IsSuccessStatusCode)
                    {
                        await HandleErrorAsync(response);
                    }

                    return await response.Content.ReadAsStringAsync();
                case Method.Post:
                    var content = new StringContent(payload, Encoding.UTF8, "application/json");
                    response = await _client.PostAsync($"{endpoint}?{additionalParamaters}", content);
                    return await response.Content.ReadAsStringAsync();
                default:
                    throw new ArgumentException($"Method {method} is not supported");
            }
        }

        private async Task HandleErrorAsync(HttpResponseMessage response)
        {
            var raw = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(raw);

            if (((int) response.StatusCode).ToString().StartsWith("5"))
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
            }
        }
    }
}