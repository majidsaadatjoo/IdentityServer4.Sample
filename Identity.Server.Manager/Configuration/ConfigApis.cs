using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Server.Manager.Configuration
{
    public static class ConfigApis
    {
        public static IEnumerable<ApiResource> Get =>
         new[]
         {
                 new ApiResource("Resources"){
                     Scopes =
                     {
                         "Permission",
                         //Define Claim Types
                         //...
                     },
                     
                 },

         };
    }
}
