using System;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace embedded_form_example.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private String getFormToken()
        {
            Console.WriteLine("application started");
            String request = "{\"idUser\":23720,\"amount\":10000,\"currency\":\"EUR\",\"customer\":{\"email\":\"lm@lyra-network.es\",\"billingDetails\":{\"firstName\":\"François\",\"lastName\":\"TestLyra\",\"phoneNumber\":\"0102030405\"}}}";
            String url = "https://api.payzen.eu/api-payment/V4/Charge/CreatePayment";

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers["Content-Type"] = "application/json; Charset=utf-8";
                    client.Headers["authorization"] = $"Basic Njk4NzYzNTc6dGVzdHBhc3N3b3JkX0RFTU9QUklWQVRFS0VZMjNHNDQ3NXpYWlEyVUE1eDdN";

                    // Download data.
                    String res = client.UploadString(url, "POST", request);
                    var json = JObject.Parse(res);
                    String formToken = (String) json["answer"]["formToken"];
                    Console.WriteLine(formToken);

                    return formToken;

                }
                catch (WebException e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
            }
        }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String formToken = this.getFormToken();
            ViewData["formToken"] = formToken;
        }
    }
}
