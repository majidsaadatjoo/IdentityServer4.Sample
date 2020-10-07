using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Idenitity.Server.Core.Models;
using Idenitity.Server.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly UserManager<MyUsers> _userManager = null;
        private readonly RoleManager<MyRoles> _roleManager = null;

        public AccountingController(UserManager<MyUsers> userManager, RoleManager<MyRoles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(User _user)
        {
            if (!_user.Password.Equals(_user.RepeatPassword))
                return BadRequest("Password not Matched,Please make sure your passwords match");

            if (await _userManager.FindByNameAsync(_user.UserName)!= null)
                return BadRequest("Username already exists, Please try with another one");

            if (await _userManager.FindByEmailAsync(_user.Email) != null)
                return BadRequest("Email already exists, Please try with another one");

            MyUsers temp = new MyUsers();
            temp.UserName = _user.UserName;
            temp.Email = _user.Email;
            var result = await _userManager.CreateAsync(temp, _user.Password);

            if (!result.Succeeded)
                return BadRequest(String.Join(", ", result.Errors));

            return Ok();
        } 
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsync(Role _Role)
        {
            if (await _roleManager.FindByNameAsync(_Role.RoleName)!= null)
                return BadRequest("RoleName already exists, Please try with another one");
            MyRoles temp = new MyRoles();
            temp.Name = _Role.RoleName;      
            await _roleManager.CreateAsync(temp);
            return Ok();
        } 
        
        [HttpPost("AddUserClaim")]
        public async Task<IActionResult> CreateUserClaimAsync(UserClaim _UserClaim)
        {
            var _user = await _userManager.FindByNameAsync(_UserClaim.UserName);
            if (_user == null)
                return BadRequest("UserName does Not exsit");          
            await _userManager.AddClaimAsync(_user,new Claim(_UserClaim.Claim.Type,_UserClaim.Claim.Value) );
            return Ok();
        } 
        
        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRoleAsync(UserRole _UserRole)
        {
            var _user = await _userManager.FindByNameAsync(_UserRole.UserName);
            var _role = await _roleManager.FindByNameAsync(_UserRole.RoleName);
            if (_user == null)
                return BadRequest("UserName does Not exsit"); 
            if (_role == null)
                return BadRequest("RoleName does Not exsit");          
            await _userManager.AddToRoleAsync(_user,_role.Name);
            return Ok();
        }
        [HttpPost("AddRoleClaim")]
        public async Task<IActionResult> AddRoleClaimAsync(RoleClaim _roleClaim)
        {
            var _role = await _roleManager.FindByNameAsync(_roleClaim.RoleName);
            if (_role == null)
                return BadRequest("RoleName does Not exsit");
            await _roleManager.AddClaimAsync(_role, new Claim(_roleClaim.Claim.Type, _roleClaim.Claim.Value));
            return Ok();
        }

    }
}