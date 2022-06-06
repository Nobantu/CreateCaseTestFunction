using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction2.Services.Models;
public class RequestContactPerson
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string ContactNumber { get; set; }
    public string AlternativeContactNumber { get; set; }
    public string ContactEmails { get; set; }
}