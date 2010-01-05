using System;
using System.Web.Routing;

namespace MVCommand.Commands
{
    /// <summary>
    /// Class used by commands to redirect to a givem context/event once they execute.  It populates an IRedirect object, which the front controller
    /// then uses to redirect to the appropriate url
    /// </summary>
    public static class Redirector
    {
        public static IRedirect Redirect(string context, string @event)
        {
            var routeData = new RouteValueDictionary { { "context", context }, { "event", @event } };
            var path = RouteTable.Routes.GetVirtualPath(null, routeData);
            if (path == null)
            {
                throw new InvalidOperationException(string.Format("Matching route can't be found for context \"{0}\" and event \"{1}\"", context, @event));
            }
            var redirect = new Redirect(path.VirtualPath);
            return redirect;
        }

        public static IRedirect Redirect(string pathToRedirectTo)
        {
            return new Redirect(pathToRedirectTo);
        }
    }
}