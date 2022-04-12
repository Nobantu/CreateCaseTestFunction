using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace TestFunction2;

public sealed class CreateCase
{
    private readonly ILogger<CreateCase> _logger;

    public CreateCase(ILogger<CreateCase> logger)
    {
        _logger = logger;
    }

    [FunctionName("CreateCase")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Case"})]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    //[OpenApiParameter(name: "OperationNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **operationNodeId** parameter")]
    //[OpenApiParameter(name: "ClientNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **clientNodeId** parameter")]
    //[OpenApiParameter(name: "CreatorNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **creatorNodeId** parameter")]
    //[OpenApiRequestBody(contentType: "application/json",bodyType: typeof(Customer),Description ="Customer Details",Required =true)]
    //[OpenApiRequestBody(contentType: "", bodyType: typeof(Vehicle), Description = "Vehicle Details", Required = true)]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]

    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function , "post", Route = null)] HttpRequest req)
    {
        //    string opID = req.Query["OperationNodeId"];
        //    string clientID = req.Query["ClientNodeId"];
        //    string creatorID = req.Query["CreatorNodeID"];
        //    string errorMessage = req.Query["Error"];

        //string customerDetails = await new StreamReader(req.Body).ReadToEndAsync();

        //dynamic customer = JsonConvert.DeserializeObject(customerDetails);
        // return (ActionResult)new OkObjectResult(new { customer = customer });
        await Task.CompletedTask;
        return new OkObjectResult("Create Case");
    }
}