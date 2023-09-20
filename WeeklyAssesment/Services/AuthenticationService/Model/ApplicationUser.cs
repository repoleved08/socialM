using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Model
{
    public class ApplicationUser : IdentityUser
    {
        // you can add more properties here
        public string Name { get; set; } = string.Empty;
    }
}
