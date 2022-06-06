using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction2.Services.Models;
public class CompletedAtLocation
{
    public string ExtraLocationInformation { get; set; }
    public int lat { get; set; }
    public int Lon { get; set; }
    public string ExternalReference { get; set; }
}