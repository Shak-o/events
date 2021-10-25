using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AcademyApp.Models
{
    public class UserIdentity : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
