using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Server.Manager.Configuration
{
    public static class ConfigClients
    {
        public static IEnumerable<Client> Get =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "Mvc",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "Resources", "openid", "profile"},
             
                }
            };
    }
}
