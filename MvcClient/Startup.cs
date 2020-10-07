using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;
using Thinktecture.IdentityModel.Extensions;
[assembly: OwinStartup(typeof(MvcClient.Startup))]

namespace MvcClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44348/",
                RequiredScopes = new string[] { "Resources" },
                ClientId = "mvc",
                ClientSecret = "secret",
                DelayLoadMetadata = true,
            }) ;
           // app.UseResourceAuthorization(new Thinktecture.IdentityModel.Owin.ResourceAuthorization );
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
