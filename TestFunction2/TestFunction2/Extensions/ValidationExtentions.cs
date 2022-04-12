using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction2.Extensions;

public static class ValidationExtentions
{
    //if (Regex.IsMatch(opID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
    //{
    //    if (Regex.IsMatch(clientID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
    //    {
    //        if (Regex.IsMatch(creatorID, @"^[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]-[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]+[A-Za-z0-9]$") == true)
    //        {
    //            if (opID == clientID || opID == creatorID || clientID == creatorID)
    //            {
    //                return (ActionResult)new OkObjectResult(new
    //                {
    //                    ErrorMessage = "Parameters can not be the same value!!"
    //                });
    //            }
    //            return (ActionResult)new OkObjectResult(new
    //            {   
    //                operationID = opID,
    //                clientID = clientID, 
    //                creatorID = creatorID
    //            });
    //        }
    //        else
    //        {
    //            return (ActionResult)new OkObjectResult(new
    //            {
    //                ErrorMessage = "Please input the creatorID in the format 00000000-0000-0000-0000-000000000000"
    //            });
    //        }
    //        return (ActionResult)new OkObjectResult(new
    //        {
    //            operationID = opID,
    //            clientID = clientID
    //        });
    //    }
    //    else
    //    {
    //        return (ActionResult)new OkObjectResult(new
    //        {
    //            ErrorMessage = "Please input the clientID in the format 00000000-0000-0000-0000-000000000000"
    //        });
    //    }
    //    return (ActionResult)new OkObjectResult(new
    //    {
    //        operationID = opID
    //    });
    //}
    //    return (ActionResult)new OkObjectResult(new
    //    { 
    //       ErrorMessage = "Please input the OperationNodeId in the format 00000000-0000-0000-0000-000000000000"
    //    });
}