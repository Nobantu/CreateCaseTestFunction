using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace TestFunction2.Services.Models
{
    public class AccidentTowService
    {
        private readonly ILogger<AccidentTowService> _logger;

        public AccidentTowService(ILogger<AccidentTowService> log)
        {
            _logger = log;
        }
        [FunctionName("AccidentTowService")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AccientTowServices" })]
        [OpenApiSecurity("Bearer_auth", SecuritySchemeType.ApiKey, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT", Name = "Token", In = OpenApiSecurityLocationType.Header)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AccientTow), Required = true, Description = "Create accident to services")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var token = req.Headers?["Token"];
            var AccidentTowDetails = await new StreamReader(req.Body).ReadToEndAsync();
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                var baseUrl = new Uri("https://yttriumstaging.dreamtec.co.za/api/AccidentTowService/Create");
                HttpRequestMessage userRequest = new HttpRequestMessage(HttpMethod.Post, baseUrl);
                userRequest.Content = new StringContent(AccidentTowDetails, Encoding.UTF8, "application/json");
                //token = token.Value.ToString().Replace("Bearer", "");
                userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                responseMessage = await client.SendAsync(userRequest);
                var accidentDetails = await responseMessage.Content.ReadAsStringAsync();
                return new OkObjectResult(accidentDetails);
            }
        }
    }
}