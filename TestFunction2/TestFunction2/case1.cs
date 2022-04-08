using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Office2019.Excel.RichData;
using DocumentFormat.OpenXml.Spreadsheet;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using YamlDotNet.Core.Tokens;

namespace TestFunction2
{
    

    public class case1
    {
        private readonly ILogger<case1> _logger;

        public case1(ILogger<case1> log)
        {
            _logger = log;
        }

        //[FunctionName("case1")]
        ////Cant update anything from here as this would make the application to require a restart
        ////this portion is what appears when we load the api urlh
        //[OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        //[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        //[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        //public async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        //{
        //    CaseDetails caseInfo;
        //    caseInfo = await new StreamReader(req.Body).ReadToEndAsync();
        //    if (caseInfo.firstName == null)
        //    {
        //        return new BadRequestObjectResult("Please enter name");
        //    }
        //    if (caseInfo.surname == null)
        //    {
        //        return new BadRequestObjectResult("Please enter surname");
        //    }
        //    if (caseInfo.contactNumber.ToString() == null)
        //    {
        //        return new BadRequestObjectResult("Please enter contact number");
        //    }
        //    return (ActionResult)new OkObjectResult(new
        //    {
        //        //message = case1
        //    });


        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    string responseMessage = string.IsNullOrEmpty(name)
        //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //    : $"Hello, {name}. This HTTP triggered function executed successfully.";
        //}

        [FunctionName("CreateCase")]
        //Cant update anything from here as this would make the application to require a restart
        //this portion is what appears when we load the api urlh
        [OpenApiOperation(operationId: "Run", tags: new[] { "Case"})]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "OperationNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **operationNodeId** parameter")]
        [OpenApiParameter(name: "ClientNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **clientNodeId** parameter")]
        [OpenApiParameter(name: "CreatorNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **creatorNodeId** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function , "post", Route = null)] HttpRequest req)
        {
            
            string opID = req.Query["OperationNodeId"];
            string clientID = req.Query["ClientNodeId"];
            string creatorID = req.Query["CreatorNodeID"];
            string ErrorMessage = req.Query["Error"];
            
            if (Regex.IsMatch(opID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
            {
                if (Regex.IsMatch(clientID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
                {
                    if (Regex.IsMatch(creatorID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
                    {
                        if (opID == clientID || opID == creatorID || clientID == creatorID)
                        {
                            return (ActionResult)new OkObjectResult(new
                            {
                                ErrorMessage = "Please input the creatorID in the format 00000000-0000-0000-0000-000000000000"
                            });
                        }
                        return (ActionResult)new OkObjectResult(new
                        {   
                            operationID = opID,
                            clientID = clientID, 
                            creatorID = creatorID
                        });
                    }
                    else
                    {
                        return (ActionResult)new OkObjectResult(new
                        {
                            ErrorMessage = "Please input the creatorID in the format 00000000-0000-0000-0000-000000000000"
                        });
                    }
                    return (ActionResult)new OkObjectResult(new
                    {
                        operationID = opID,
                        clientID = clientID
                    });
                }
                else
                {
                    return (ActionResult)new OkObjectResult(new
                    {
                        ErrorMessage = "Please input the clientID in the format 00000000-0000-0000-0000-000000000000"
                    });
                }
                return (ActionResult)new OkObjectResult(new
                {
                    operationID = opID
                });
            }
                return (ActionResult)new OkObjectResult(new
                { 
                   ErrorMessage = "Please input the OperationNodeId in the format 00000000-0000-0000-0000-000000000000"
                });
            
            return new OkResult();
        }
    }
    public class Customer
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int ContactNumber { get; set; }
        public int IDNumber { get; set; }
        public int IdentificationType { get; set; }
        public bool IsPolicyHolder { get; set; }
    }
}

