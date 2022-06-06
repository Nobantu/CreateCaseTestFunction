using System;
using System.IO;
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
using TestFunction2.Authentication.Models;

namespace TestFunction2.Authentication;
public class CreateUser
{
    private readonly ILogger<CreateUser> _logger;
    public CreateUser(ILogger<CreateUser> log)
    {
        _logger = log;
    }

    [FunctionName("CreateUser")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "User" }, Summary = "Bearer aithentication token via header", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiSecurity("Bearer_auth", SecuritySchemeType.ApiKey,Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT", Name = "Token", In = OpenApiSecurityLocationType.Header)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewUser), Description = "Create new user", Required = true)]
    
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var authHeader = req.Headers?["Token"];        
        var nUser = await new StreamReader(req.Body).ReadToEndAsync();
        HttpResponseMessage response = new HttpResponseMessage();
        using (var user = new HttpClient())
        {
            var urlstring = new Uri("https://yttriumstaging.dreamtec.co.za/api/CreateUser");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, urlstring);
            httpRequest.Content = new StringContent(nUser, Encoding.UTF8, "application/json");
            //authHeader = authHeader.Value.ToString().Replace("Bearer", "");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authHeader);
            response = await user.SendAsync(httpRequest);
            var userDT = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(userDT);
        }
    }
}