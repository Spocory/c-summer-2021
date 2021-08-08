using System.Configuration;
using System.Web.Mvc;
using System.Web;

namespace HelloWorld
{
    public class AuthorizeIpAddressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get users IP Address
            string ipAddress = HttpContext.Current.Request.UserHostAddress;

            if (ipAddress == "::1")
            {
                // Send back a HTTP Status code of 403 Forbidden
                filterContext.Result = new HttpStatusCodeResult(403);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}