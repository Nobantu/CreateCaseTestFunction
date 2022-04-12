using System;

namespace TestFunction2.Authentication.Models;

public sealed class Credentials
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid ClientId { get; set; }
}