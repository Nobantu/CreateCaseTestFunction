using System;

namespace TestFunction2.Authentication.Models;

public sealed class SignInUser
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
}