using System;
using System.Web.Routing;

namespace MVCommand.Commands
{
    /// <summary>
    /// Class for returning the appropriate path to a given context and event combination
    /// </summary>
    public static class UrlGenerator
    {
        public static string GetUrlFor(string context, string @event)
        {
            var routeData = new RouteValueDictionary { { "context", context }, { "event", @event } };
            var path = RouteTable.Routes.GetVirtualPath(null, routeData);
            if (path == null)
            {
                throw new InvalidOperationException(string.Format("Matching route can't be found for context \"{0}\" and event \"{1}\"", context, @event));
            }
            return path.VirtualPath;
        }
    }
}