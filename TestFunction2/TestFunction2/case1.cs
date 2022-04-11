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

        [FunctionName("CreateCase")]
        //Cant update anything from here as this would make the application to require a restart
        //this portion is what appears when we load the api urlh
        [OpenApiOperation(operationId: "Run", tags: new[] { "Case"})]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "OperationNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **operationNodeId** parameter")]
        [OpenApiParameter(name: "ClientNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **clientNodeId** parameter")]
        [OpenApiParameter(name: "CreatorNodeId", In = ParameterLocation.Query, Required = true, Type = typeof(char), Description = "The **creatorNodeId** parameter")]
        [OpenApiRequestBody(contentType: "application/json",bodyType: typeof(Customer),Description ="Parameters",Required =true)]
        [OpenApiResponseBody()]
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
                                ErrorMessage = "Parameters can not be the same value!!"
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
    public class vehicle
    {
        public string LicensePlateNumber { get; set; }
        public string make { get; set; }
        public string Model { get; set; }
        public string MnCode { get; set; }
        public DateOnly Year { get; set; }
        public string Vin { get; set; }
        public int VehicleCode { get; set; }
        public int WriteOffIndicator { get; set; }
        public int VehicleType { get; set; }
    }
    public class Account
    {
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        public int IdentificationType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public char ClientId { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyVehicleExternalSystemId { get; set; }
        public string PolicyDriverExternalSystemId { get; set; }
    }
}

