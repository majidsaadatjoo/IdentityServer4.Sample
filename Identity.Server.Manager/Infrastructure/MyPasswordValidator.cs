using Idenitity.Server.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Server.Manager.Infrastructure
{
    public class MyPasswordValidator : PasswordValidator<MyUsers>
    {
        
        public override async Task<IdentityResult> ValidateAsync(UserManager<MyUsers> manager, MyUsers user, string password)
        {
            var Result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = Result.Succeeded? new List<IdentityError>() : Result.Errors.ToList();
            if (user.UserName.ToLower().Contains(password.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "UsernameContainsPassword",
                    Description = "Username Cannot Contain Password"
                });
            }
            if (password.Contains("asd"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordIsSequence",
                    Description = "Password Cannot Contain String Sequence"
                });


            }
            if (password.Contains("123456"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordIsSequence",
                    Description = "Password Cannot Contain Numeric Sequence"
                });
            }
            return await Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }




}
