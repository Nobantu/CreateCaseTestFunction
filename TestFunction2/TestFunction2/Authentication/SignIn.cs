using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Credentials), Description = "SignIn Credentials", Required = true)]
   // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var credentials = JsonConvert.DeserializeObject<Credentials>(requestBody);

        using (var user = new HttpClient())
        {
            user.BaseAddress = new Uri("http://localhost:7071/");

            user.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await user.PostAsJsonAsync(requestBody, credentials);

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                credentials = JsonConvert.DeserializeObject<Credentials>(result);
                //it shouldnt return credentials. instead it should create a token like in Yttrium
                return new OkObjectResult(credentials);
            }
            else
                return (IActionResult)new OkObjectResult(new
                {
                    errorMessage = "No results to display"
                });
        }
        return new OkObjectResult(credentials);
    }
}