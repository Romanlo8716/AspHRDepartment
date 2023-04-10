using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Laba1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Laba1User class
public class Laba1User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

