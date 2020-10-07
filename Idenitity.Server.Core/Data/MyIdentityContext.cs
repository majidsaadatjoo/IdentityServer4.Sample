
using Idenitity.Server.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Idenitity.Server.Core.Data
{
    //public class MyIdentityContext : IdentityDbContext<MyUsers, MyRoles, int, MyUserClaim, MyUserRoles, MyUserLogin, MyRoleClaims, MyUserToken>
    public class MyIdentityContext : IdentityDbContext<MyUsers,MyRoles,int>
    {
        public MyIdentityContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityDbConnetction"));
            base.OnConfiguring(optionsBuilder); 
        }
    }
}
