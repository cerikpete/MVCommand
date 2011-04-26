using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCommand.Views.ViewEngine
{
    /// <summary>
    /// Custom view engine used to appropriately locate partial views in folders besides "Shared" for the MVCommand framework
    /// </summary>
    public class CommandViewEngine : WebFormViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var context = controllerContext.RouteData.GetRequiredString("context");

            // Insert current context as a possible location to retrieve a partial view from            
            var currentFormats = PartialViewLocationFormats.ToList();
            foreach (var format in FormatsForPossiblePartials(context))
            {
                if (!currentFormats.Contains(format))
                {
                    currentFormats.Add(format);
                }
            }
            
            PartialViewLocationFormats = currentFormats.ToArray();

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        private IEnumerable<string> FormatsForPossiblePartials(string context)
        {
            var possibleExtensions = new List<string> {"ascx", "cshtml"};
            foreach (var extension in possibleExtensions)
            {
                yield return string.Format("~/Views/" + context + "/{0}.{1}", extension);
            }
        }
    }
}