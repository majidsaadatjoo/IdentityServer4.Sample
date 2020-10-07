using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Server.Manager.Configuration
{
    public static class ConfigIds
    {
        public static IEnumerable<IdentityResource> Get =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
    }
}
