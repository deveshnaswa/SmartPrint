using SmartPrint.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartPrint.Common.Filters
{
    public class AdminAuthorizationFilterAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsUserAdmin())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Auth", action = "Unauthorised"}));
            }
        }

        protected List<int> GetLoggedInUserRoles()
        {
            var result = new List<int>();
            var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var roles = identity.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
                if (roles.Count != 0)
                {
                    result = roles.Select(x => int.Parse(x.Value)).ToList();
                }
            }
            return result;
        }

        protected bool IsUserAdmin()
        {
            return GetLoggedInUserRoles().Contains((int)Common.Enums.UserType.Administrator);
        }
    }
}