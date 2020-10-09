using Idenitity.Server.Core.Models;
using Identity.Server.Manager.Extension;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Server.Manager.Infrastructure
{
    public class MyTokenRequesValidatior : ICustomTokenRequestValidator
    {
        private readonly UserManager<MyUsers> userManager;

        public MyTokenRequesValidatior(UserManager<MyUsers> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var user = await userManager.FindByNameAsync(context.Result.ValidatedRequest.UserName);
         

            if (user.IsActive)
            {              
                context.Result.IsError = true;
                context.Result.ErrorDescription = "There is already an active session using this user name. Please close that session first and re-login ";
                context.Result.Error = "already an active session";
              await  userManager.AccessFailedAsync(user);

            }
            else
            {
              await  userManager.Activate(user);
                
            }
        
            
        }
    }

}
