using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using aspnetcore_sagepay.Models;
using aspnetcore_sagepay.Models.Request;
using aspnetcore_sagepay.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace aspnetcore_sagepay.Controllers
{
    public class PaymentController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri("https://pi-test.sagepay.com/") };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("CardholderName,CardNumber,ExpiryDate,SecurityCode")]CardholderModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var key = await GenerateMerchantSessionKey(new MerchantSessionKeyRequestModel
            {
                VendorName = "sandBox"
            });
            var cardIdentifier = GenerateCardIdentifier(model, key.MerchantSessionKey);
            return View();
        }

        public async Task<MerchantSessionKeyResponseModel> GenerateMerchantSessionKey(MerchantSessionKeyRequestModel model)
        {
            //For HTTP Basic authentication, you will need to combine into a string the “integrationKey:integrationPassword”.
            //The resulting string will have to be encoded using Base64 encoding.
            //The encoded string will have to be included in the Authorization header.
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "aEpZeHN3N0hMYmo0MGNCOHVkRVM4Q0RSRkxodUo4RzU0TzZyRHBVWHZFNmhZRHJyaWE6bzJpSFNyRnliWU1acG1XT1FNdWhzWFA1MlY0ZkJ0cHVTRHNocktEU1dzQlkxT2lONmh3ZDlLYjEyejRqNVVzNXU=");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var payload = JsonConvert.SerializeObject(model, serializerSettings);

                var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/merchant-session-keys")
                {
                    Content = new StringContent(payload, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MerchantSessionKeyResponseModel>(jsonString);
            }
            catch (Exception ex)
            {
                // log error
                throw ex;
            }
        }

        public async Task<string> GenerateCardIdentifier(CardholderModel model, string merchantSessionKey)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", merchantSessionKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var json = JsonConvert.SerializeObject(model, serializerSettings);

                var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/card-identifiers")
                {
                    Content = new StringContent($"{{\"cardDetails\": {json}}}", Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(jsonString);
            }
            catch (Exception ex)
            {
                // log error
                throw ex;
            }
        }

        public async Task<string> ProcessTransaction(TransactionModel model, string merchantSessionKey)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", merchantSessionKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var json = JsonConvert.SerializeObject(model, serializerSettings);

                var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/card-identifiers")
                {
                    Content = new StringContent($"{{\"cardDetails\": {json}}}", Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(jsonString);
            }
            catch (Exception ex)
            {
                // log error
                throw ex;
            }
        }
    }
}