using SmartPrint.App_Start;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Runtime.Caching;
using SmartPrint.Common.Enums;
using System;
using SmartPrint.Common;

namespace SmartPrint
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;

            MemoryCache.Default.Add(Constants.RecordStatusListName, EnumInfo.GetList<RecordStatus>(), DateTimeOffset.MaxValue);
            MemoryCache.Default.Add(Constants.TransactionStatusListName, EnumInfo.GetList<TransactionStatus>(), DateTimeOffset.MaxValue);
            MemoryCache.Default.Add(Constants.UserTypeListName, EnumInfo.GetList<UserType>(), DateTimeOffset.MaxValue);
            MemoryCache.Default.Add(Constants.TransactionTypeListName, EnumInfo.GetList<TransactionType>(), DateTimeOffset.MaxValue);
            //For document viewer
            //  DocuViewareManager.SetupConfiguration();
            //025e99458a3c490ea0609e65f0b4f240bf35d80ee1283deeGx4ZcGz7zE8pzwYyLtCWhhqcW/TElpE8vdNooePcDI5/eFimqlbgw4626xZ5akP8
            //DocuViewareLicensing.RegisterKEY("025e99458a3c490ea0609e65f0b4f240bf35d80ee1283deeGx4ZcGz7zE8pzwYyLtCWhhqcW/TElpE8vdNooePcDI5/eFimqlbgw4626xZ5akP8"); 
            // Please enter your license key. Claim your free DocuVieware Lite license key here: http://www.docuvieware.com/docuvieware-lite/
        }

        public static string GetDocumentsDirectory()
        {
            return HttpRuntime.AppDomainAppPath + "\\UserFileUploads";
        }

        /// <summary>
        /// This methods simply returns a string containing the folder that will be use for DocuVieware cache.
        /// In our case, it returns the Cache folder that is in this project.
        /// </summary>
        public static string GetCacheDirectory()
        {
            return HttpRuntime.AppDomainAppPath + "\\Cache";
        }

        /// <summary>
        /// This methods simply returns a string containing the folder that will contain extra fonts.
        /// In our case, it returns null because we do not use this feature.
        /// </summary>
        public string GetFontsDirectory()
        {
            return null;
        }
    }
}
