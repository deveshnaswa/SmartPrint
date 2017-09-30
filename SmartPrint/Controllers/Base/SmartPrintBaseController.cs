using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace SmartPrint.Controllers.Base
{
    public abstract class SmartPrintBaseController : Controller
    {
        protected int GetLoggedInUserId()
        {
            return int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}