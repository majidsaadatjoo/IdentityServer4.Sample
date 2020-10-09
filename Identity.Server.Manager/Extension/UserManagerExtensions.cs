
using Idenitity.Server.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Server.Manager.Extension
{
    public static class UserManagerExtensions
    {
        public static  Task<bool> IsActive(this UserManager<MyUsers> userManager, MyUsers users) => Task.FromResult(userManager.FindByNameAsync(users.UserName).Result.IsActive);
        public static async Task Activate(this UserManager<MyUsers> userManager, MyUsers users)
        {
            users.IsActive = true;
            await userManager.UpdateAsync(users);           
        }
    }
}
