using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCommand.Views.ViewEngine
{
    /// <summary>
    /// Custom view engine used to appropriately locate partial views in folders besides "Shared" for the MVCommand framework
    /// </summary>
    public class CommandViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var context = controllerContext.RouteData.GetRequiredString("context");

            // Insert current context as a possible location to retrieve a partial view from            
            var currentFormats = ViewLocationFormats.ToList();
            foreach (var format in FormatsForPossiblePartials(context,"aspx", "cshtml"))
            {
                if (!currentFormats.Contains(format))
                {
                    currentFormats.Add(format);
                }
            }

            ViewLocationFormats = currentFormats.ToArray();
            //string formats = ViewLocationFormats.Aggregate(string.Empty, (current, viewLocationFormat) => current + (", " + viewLocationFormat));
            //throw new Exception(string.Format("I dont get it: formats: {0} \n viewName: {1}", formats, viewName));
            return base.FindView(controllerContext, viewName, masterName,  useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var context = controllerContext.RouteData.GetRequiredString("context");

            // Insert current context as a possible location to retrieve a partial view from            
            var currentFormats = PartialViewLocationFormats.ToList();
            foreach (var format in FormatsForPossiblePartials(context, "ascx", "cshtml"))
            {
                if (!currentFormats.Contains(format))
                {
                    currentFormats.Add(format);
                }
            }
            
            PartialViewLocationFormats = currentFormats.ToArray();

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        private IEnumerable<string> FormatsForPossiblePartials(string context, params string[] possibleExtensions)
        {
            foreach (var extension in possibleExtensions)
            {
                yield return string.Format("~/Views/" + context + "/{0}.{1}","{0}", extension);
            }
        }
    }
}