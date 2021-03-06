﻿using System.Linq;
using System.Web.Mvc;
using MVCommand.Logging;

namespace MVCommand.Views.ViewEngine
{
    /// <summary>
    /// Custom view engine used to appropriately locate partial views in folders besides "Shared" for the MVCommand framework
    /// </summary>
    public class CommandRazorViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var context = controllerContext.RouteData.GetRequiredString("context");

            // Insert current context as a possible location to retrieve a partial view from
            var partialLocationForContextFormat = "~/Views/" + context + "/{0}.cshtml";
            var currentFormats = ViewLocationFormats.ToList();
            if (!currentFormats.Contains(partialLocationForContextFormat))
            {
                currentFormats.Insert(0, partialLocationForContextFormat);
            }
            ViewLocationFormats = currentFormats.ToArray();
            foreach (var currentFormat in currentFormats)
            {
                Log<CommandViewEngine>.Debug("View array format item: " + currentFormat);
            }

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var context = controllerContext.RouteData.GetRequiredString("context");

            // Insert current context as a possible location to retrieve a partial view from
            var partialLocationForContextFormat = "~/Views/" + context + "/{0}.cshtml";
            var currentFormats = PartialViewLocationFormats.ToList();
            if (!currentFormats.Contains(partialLocationForContextFormat))
            {
                currentFormats.Insert(0, partialLocationForContextFormat);
            }
            PartialViewLocationFormats = currentFormats.ToArray();
            foreach (var currentFormat in currentFormats)
            {
                Log<CommandViewEngine>.Debug("Partial view array format item: " + currentFormat);
            }

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }
    }
}