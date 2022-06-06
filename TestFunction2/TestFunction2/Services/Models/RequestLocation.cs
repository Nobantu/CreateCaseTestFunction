using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction2.Services.Models;
public class RequestLocation
{
    public string ExternalLocationInformation { get; set; }
    public int Lat { get; set; }
    public int Lon { get; set; }
    public string ExternalReference { get; set; }
}