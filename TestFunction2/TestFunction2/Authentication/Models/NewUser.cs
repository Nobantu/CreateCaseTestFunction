using System;

namespace TestFunction2.Authentication.Models;

public sealed class NewUser
{
    public Guid ClientId { get; set; }
    public string EmailAddress { get; set; }
    public string UniqueReferenceNumber { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid OperationsNodeId { get; set; }

}
