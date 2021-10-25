using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models;
using AcademyApp.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AcademyApp.Infrastructure
{
    public static class Helper
    {
        public static async Task<UserIdentity> GetUpdateableUser(UpdateViewModel user, UserManager<UserIdentity> userManager)
        {
            var identityUser = await userManager.FindByNameAsync(user.UserName);
            identityUser.Name = user.Name;
            identityUser.LastName = user.LastName;
            identityUser.Password = user.Password;
            identityUser.UserName = user.UserName;
            identityUser.Email = user.Email;
            
            return identityUser;
        }
    }
}
