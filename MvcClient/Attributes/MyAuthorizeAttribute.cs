using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace MvcClient.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public override object TypeId => base.TypeId;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var claims = filterContext.HttpContext.User.Identity as ClaimsIdentity;

            base.OnAuthorization(filterContext);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }
    }
}