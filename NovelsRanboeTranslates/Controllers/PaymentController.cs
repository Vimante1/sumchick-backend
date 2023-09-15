using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NovelsRanboeTranslates.Domain.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("/Paymant")]
    public class PaymentController : ControllerBase
    {
        private readonly PaypalCredentials _paypal;
    
        public PaymentController(IOptions<PaypalCredentials> options)
        {
            _paypal = options.Value;
        }

        [HttpGet]
        [Route("PaypalPayment")]
        public async Task<IActionResult> PaypalPayment(string order_id, string token)
        {
            var paypalURL = "https://api-m.sandbox.paypal.com/v2/checkout/orders/";
            string description;
            decimal value;
            #region Request to pp api

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {

                    var response = await httpClient.GetAsync(paypalURL + order_id);
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic responseBody = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject(responseBody);
                        description = data.purchase_units[0].description;
                        value = data.purchase_units[0].amount.value;
                    }
                    else
                    {
                        Console.WriteLine($"Error 1: {response.StatusCode}");
                        return NotFound("SomethingWrong");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error 2: {ex.Message}");
                    return NotFound("SomethingWrong");

                }
            }

            #endregion

            

            return Ok();
        }
    }
}
