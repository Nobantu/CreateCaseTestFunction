using System;

namespace TestFunction2.Cases.Models;

public class Account
{
    public string CompanyName { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string IdNumber { get; set; }
    public int IdentificationType { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ContactNumber { get; set; }
    public char ClientId { get; set; }
    public string PolicyNumber { get; set; }
    public string PolicyVehicleExternalSystemId { get; set; }
    public string PolicyDriverExternalSystemId { get; set; }
}