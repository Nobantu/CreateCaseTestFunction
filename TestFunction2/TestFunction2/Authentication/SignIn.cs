using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TestFunction2.Authentication.Models;

namespace TestFunction2.Authentication;
public class SignIn
{
    private readonly ILogger<SignIn> _logger;

    public SignIn(ILogger<SignIn> log)
    {
        _logger = log;
    }

    [FunctionName("SignIn")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
   // [OpenApiSecurity("Bearer Token", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Credentials), Description = "SignIn Credentials", Required = true)]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        HttpResponseMessage response = new HttpResponseMessage();
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var credentials = JsonConvert.DeserializeObject<Credentials>(requestBody);
        using (var user = new HttpClient())
        {
            user.BaseAddress = new Uri("https://yttriumstaging.dreamtec.co.za/api/");
            string json = JsonConvert.SerializeObject(credentials);
            var answer = new StringContent(json, Encoding.UTF8, "application/json");
            response = await user.PostAsync("SignIn", answer);
            var res = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(res);
        }   
    }
}