using System;
using System.Collections.Generic;
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
using TestFunction2.Cases.Models;

namespace TestFunction2;

public class CreateCase
{
    private readonly ILogger<CreateCase> _logger;
    public CreateCase(ILogger<CreateCase> logger)
    {
        _logger = logger;
    }
    [FunctionName("CreateCase")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Case" })]
    [OpenApiSecurity("Bearer_auth", SecuritySchemeType.ApiKey, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT", Name = "Token", In = OpenApiSecurityLocationType.Header)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ExternalElements) , Description = "Add case details", Required = true)]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function , "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var token = req.Headers?["Token"];
        var caseDetails = await new StreamReader(req.Body).ReadToEndAsync();
        HttpResponseMessage response = new HttpResponseMessage();

        using (var client = new HttpClient())
        {
            var baseUrl = new Uri("https://yttriumstaging.dreamtec.co.za/api/Case/Create");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            request.Content = new StringContent(caseDetails, Encoding.UTF8,"application/json");
            //token = token.Value.ToString().Replace("Bearer", "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.SendAsync(request);
            var results = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(results);
        }
    }
}