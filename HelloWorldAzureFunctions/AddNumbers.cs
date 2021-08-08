using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HelloWorldAzureFunctions
{
    public static class AddNumbers
    {
        [FunctionName("AddNumbers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get","post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string number1 = req.Query["number1"];
            string number2 = req.Query["number2"];

            int pnumber1 = data.number1;
            int pnumber2 = data.number2;

            
            int total = data.number1 + data.number2;
            string finaltotal = total.ToString();

            return new OkObjectResult(finaltotal);
        }
    }
}
