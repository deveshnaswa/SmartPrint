using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SmartPrint.Controllers.Base
{
    public abstract class SmartPrintBaseController : Controller
    {
        public SmartPrintBaseController()
        {
            ViewBag.IsUserAdmin = IsUserAdmin();
        }

        List<int> _loggedInRoles = null;
        protected int GetLoggedInUserId()
        {
            return int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        protected int GetLoggedInUserName()
        {
            return int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserName());
        }

        protected string GetLoggedInUserEmail()
        {
            var result = string.Empty;
            var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var emails = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                if (emails != null)
                {
                    result = emails.Value;
                }
            }
            return result;
        }

        protected List<int> GetLoggedInUserRoles()
        {
            if (_loggedInRoles == null)
            {
                var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var roles = identity.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
                    if (roles.Count != 0)
                    {
                        _loggedInRoles = roles.Select(x=>int.Parse(x.Value)).ToList();
                    }
                }
            }
            return _loggedInRoles;
        }

        protected bool IsUserAdmin()
        {
            return GetLoggedInUserRoles().Contains((int)Common.Enums.UserType.Administrator);
        }

        protected string GetUploadFolderName()
        {
            return "UserFileUploads";
        }
    }
}