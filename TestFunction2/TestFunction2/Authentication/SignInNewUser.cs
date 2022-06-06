using System;
using System.IO;
using System.Net;
using System.Net.Http;
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
using TestFunction2.Authentication.Models;
using System.Net.Http.Headers;

namespace TestFunction2.Authentication;
public class SignInNewUser
{
    private readonly ILogger<SignInNewUser> _logger;
    public SignInNewUser(ILogger<SignInNewUser> log)
    {
        _logger = log;
    }
    [FunctionName("SignInNewUser")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
    [OpenApiSecurity("Bearer_auth", SecuritySchemeType.ApiKey, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT", Name = "Token", In = OpenApiSecurityLocationType.Header)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SignInUser), Required = true, Description = "SignIn a new User")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var token = req.Headers?["Token"];
        var bodyContent = await new StreamReader(req.Body).ReadToEndAsync();
        HttpResponseMessage responseMessage = new HttpResponseMessage();
        
        using (var client = new HttpClient())
        {
            var baseUrl = new Uri("https://yttriumstaging.dreamtec.co.za/api/SignInUser");
            HttpRequestMessage userRequest = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            userRequest.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            //token = token.Value.ToString().Replace("Bearer", "");
            userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            responseMessage = await client.SendAsync(userRequest);
            var newUserToken = await responseMessage.Content.ReadAsStringAsync();
            return new OkObjectResult(newUserToken);
        }
    }
}