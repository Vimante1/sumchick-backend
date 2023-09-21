using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NovelsRanboeTranslates.Domain.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NovelsRanboeTranslates.Services.Interfraces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovelsRanboeTranslates.Controllers
{
    [ApiController]
    [Route("/Paymant")]
    public class PaymentController : ControllerBase
    {
        private readonly PaypalCredentials _paypal;
        private readonly AdvCashPassword _AdvCash;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public PaymentController(IOptions<AdvCashPassword> advOptions, IOptions<PaypalCredentials> ppOptions, IPaymentService service, IUserService userService)
        {
            _paymentService = service;
            _userService = userService;
            _paypal = ppOptions.Value;
            _AdvCash = advOptions.Value;
        }

        [HttpGet]
        [Route("PaypalPayment")]
        public async Task<IActionResult> PaypalPayment(string order_id, string token)
        {
            var paypalURL = "https://api-m.sandbox.paypal.com/v2/checkout/orders/";
            string login;
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
                        login = data.purchase_units[0].description;
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
                    return BadRequest("SomethingWrong");

                }
            }

            #endregion

            try
            {
                if (!await _paymentService.PaymentAlreadyContain(order_id, token))
                {
                    var addToLog = await _paymentService.AddToLogs(new TransactionLog
                    {
                        TransactionId = order_id,
                        PaymentToken = token,
                        Amount = value,
                        PayerLogin = login,
                        Platform = "PayPal"
                    });
                    if (!addToLog)
                    {
                        return BadRequest("Something wrong with add to logs");

                    }
                    var addToBalance = await _userService.AddToBalance(login, value);
                    if (!addToBalance)
                    {
                        return BadRequest("Something wrong with add to balance");
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("This operation has already taken place");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something wrong");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AdvCashStatus")]
        public async Task<IActionResult> AdvCashStatus([FromBody] AdvCashStatus Status)
        {
            var mySha256 = Status.getHash(_AdvCash.Password);
            if (mySha256 == Status.ac_hash)
            {
                await _userService.AddToBalance(Status.userLogin ,Status.ac_amount);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
