using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.DTOs;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger _logger;
        const string endpointSecret = "whsec_6e953b24284c4a97a33c1a50e68cd8f8fc79ca29fe3d36df62a1e780e6c008af";

        public PaymentsController(IPaymentService paymentService, ILogger logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdateIntentPayment(CustomerBasketDTO input)
            => Ok(await _paymentService.CreateOrUpdateIntentPayment(input));

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;

                // Handle the event
                if (stripeEvent.Type == "payment_intent.canceled")
                {
                    _logger.LogInformation($"Payment Canceled");
                }
                else if (stripeEvent.Type == "payment_intent.created")
                {
                    _logger.LogInformation($"Payment Created");
                }
                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    _logger.LogInformation($"Payment Failed : {paymentIntent.Id}");

                    var order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);

                    _logger.LogInformation($"Order Updated TO Payment Failed : {order.Id}");
                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    _logger.LogInformation($"Payment Succeeded : {paymentIntent.Id}");

                    var order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);

                    _logger.LogInformation($"Order Updated TO Payment Succeeded : {order.Id}");
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}
