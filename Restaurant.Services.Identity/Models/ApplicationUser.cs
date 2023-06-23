using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace Restaurant.Services.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
