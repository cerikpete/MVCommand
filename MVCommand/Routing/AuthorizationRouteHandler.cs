using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCommand.Routing
{
    public class AuthorizationRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var user = requestContext.HttpContext.User.Identity;
            if (!user.IsAuthenticated)
            {
                requestContext.HttpContext.Response.StatusCode = 0x191;
            }
            return base.GetHttpHandler(requestContext);
        }
    }
}